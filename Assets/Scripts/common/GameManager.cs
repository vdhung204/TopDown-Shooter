using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text txtRound;
    public Text txtScore;
    public Text txtWave;
    public List<GameObject> enemies;
    public Dictionary<string, GameObject> dictEnemyAndTransform = new ();
    public GameObject[] player;
    public Text txtLevel;
    private LevelConfigData waveSpawn;
    private int currentWave = 1;
    private int levelConfig = 1;
    private int levelPlayer = 1;
    private int expPlayer = 0;
    private int EXPNEED;
    private int countEnemy ;
    private int countEnemyWave =0 ;
    private int currentEnemy = 0;
    private int score = 0;
    public GameObject panelEndGame; 
    public GameObject popupPause; 
    private bool isWin;
    public Sprite iconPause;
    public Sprite iconReSume;
    public Image imgPause;
    public Button btnPause;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        waveSpawn = Resources.Load<LevelConfigData>("CSV_Data/LevelConfigData");

        
        this.RegisterListener(EventID.RemoveEnemyTransform, (sender, param) => OnenemyDie((string)param));
    }
    private void Start()
    {
        isWin = false;
        panelEndGame.SetActive(false);
        popupPause.SetActive(false);
        SpawnPlayer();
        currentWave = 1;
        btnPause.onClick.AddListener(PauseGame);
        this.RegisterListener(EventID.PlayerUpEXP, (sender, param) => ChangeEXPPlayer((int)param));
        this.RegisterListener(EventID.PlayerUpLevel, (sender, param) => PlayerUpLevel((int)param));
        this.RegisterListener(EventID.PlayerUpScore, (sender, param) => PlayerUpScore());

        this.RegisterListener(EventID.OutOffEnemy, (sender, param) => SetCountEnemyWave((int)param));
        this.RegisterListener(EventID.PlayerDie, (sender, param) => EndGame());
        CheckToSpawnEnemies();
        txtLevel.text = $"{levelPlayer}";
        txtScore.text = $"{score}";
        txtWave.text = $"{currentWave}/{waveSpawn.GetLevelConfig(levelConfig).waveEnemy.Length}";


    }
    private void SetTxtRound()
    {
        txtRound.gameObject.SetActive(true);
        txtRound.text = $"ROUND {currentWave}";
    }
    private void OffTxtRound()
    {
        txtRound.gameObject.SetActive(false);
    }
    private void SetCountEnemyWave(int count)
    {
        this.countEnemyWave = count;
    }
    void ChangeEXPPlayer(int exp)
    {
        expPlayer += exp;
    }
    void PlayerUpScore()
    {
        score++;
        txtScore.text = $"{score}";
    }
    void SpawnPlayer()
    {
        var players = SmartPool.Instance.Spawn(player[DataAccountPlayer.PlayerInfor.character], Vector3.zero, Quaternion.identity);

        players.GetComponent<PlayerController>().InitInforPlayer(levelPlayer);
        EXPNEED = PlayerController.instance.expNeed;
        if (players.TryGetComponent(out Renderer renderer))
        {
            renderer.bounds.Encapsulate(players.transform.position);
        }
    }
    public void PlayerUpLevel(int level)
    {
        PlayerController.instance.InitInforPlayer(level);
        txtLevel.text = $"{level}";
        EXPNEED = PlayerController.instance.expNeed;
    }
    IEnumerator SpawnEnemies()
    {
        if(countEnemyWave == countEnemy)
        {
            currentWave++;
            
            countEnemyWave = 0;
            if(currentWave > waveSpawn.GetLevelConfig(levelConfig).waveEnemy.Length)
            {
                isWin = true;
                EndGame();
            }
            else
            {
                txtWave.text = $"{currentWave}/{waveSpawn.GetLevelConfig(levelConfig).waveEnemy.Length}";
                CheckToSpawnEnemies();
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartToSpawnEnemies();
            StartCoroutine(SpawnEnemies());
        }
    }
    private IEnumerator SpawnWave(float delayTime)
    {
        Invoke("SetTxtRound", 0.1f);
        Invoke("OffTxtRound", 2f);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SpawnEnemies());
    }
    private void CheckToSpawnEnemies()
    {
        var waveEnemies = waveSpawn.GetLevelConfig(levelConfig);
        if(currentWave == waveEnemies.waveEnemy.Length)
        {
            txtRound.text = $"FINAL";
        }else
        {
            txtRound.text = $"ROUND {currentWave}";
        }
           
        countEnemy = waveSpawn.GetWaveInfor(levelConfig, currentWave).spawn_enemy;

        for (int i = 0; i < waveEnemies.waveEnemy.Length; i++)
        {
            if (currentWave == waveEnemies.waveEnemy[i].wave)
            {
                this.PostEvent(EventID.CountEnemy,waveEnemies.waveEnemy[i].spawn_enemy);
                StartCoroutine(SpawnWave(waveEnemies.waveEnemy[i].delay_time));
                currentEnemy = 0;
                return;
            }

        }
    }
    public void StartToSpawnEnemies()
    {
        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("No enemies to spawn.");
            return;
        }
        if(currentEnemy >= countEnemy)
        {
            return;
        }
        int enemyIndex = Random.Range(0, enemies.Count);
        GameObject enemyPrefab = enemies[enemyIndex];
        var bgSize = BgController.instance.GetSizeBg();
        float xLimit = bgSize.x / 2 - 1f;
        float yLimit = bgSize.y / 2 - 1f;
        Vector3 spawnPos = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0);
        var enemyClone = SmartPool.Instance.Spawn(enemyPrefab, spawnPos, Quaternion.identity);
        currentEnemy++;
        enemyClone.GetComponent<EnemyController>().InitInforEnemy(currentWave);
        dictEnemyAndTransform.Add(enemyClone.gameObject.name, enemyClone);
    }

    private void OnenemyDie(string enemyInstanceId)
    {
        if (dictEnemyAndTransform.ContainsKey(enemyInstanceId))
        {
            dictEnemyAndTransform.Remove(enemyInstanceId);
        }
    } 
    private void EndGame()
    {
        Time.timeScale = 0;
        panelEndGame.SetActive(true);
        EndGameControlelr.instance.ShowEndGame(isWin, score, expPlayer);
    }
    public void RePlay()
    {
        SceneManager.LoadScene(SceneName.GamePlayScene.ToString());
        Time.timeScale = 1;
    }
    public void Exit()
    {
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        popupPause.SetActive(true);
        imgPause.sprite = iconPause;

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        popupPause.SetActive(false);
        imgPause.sprite = iconReSume;
    }
}
