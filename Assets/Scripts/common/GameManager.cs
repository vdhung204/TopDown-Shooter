using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text txtRound;
    public List<GameObject> enemies;
    public Dictionary<string, GameObject> dictEnemyAndTransform = new ();
    public GameObject player;
    
    private LevelConfigData waveSpawn;
    private int currentWave = 1;
    private int levelConfig = 1;
    private int levelPlayer = 1;
    private int expPlayer = 0;
    private int EXPNEED;
    private int countEnemy ;
    private int countEnemyWave =0 ;
    private int currentEnemy = 0;

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
        SpawnPlayer();
        currentWave = 1;
        this.RegisterListener(EventID.PlayerUpEXP, (sender, param) => ChangeEXPPlayer((int)param));
        this.RegisterListener(EventID.PlayerUpLevel, (sender, param) => PlayerUpLevel((int)param));
        this.RegisterListener(EventID.OutOffEnemy, (sender, param) => SetCountEnemyWave((int)param));
        this.RegisterListener(EventID.PlayerDie, (sender, param) => OnPlayerDie());
        CheckToSpawnEnemies();

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
    void SpawnPlayer()
    {
        var players = SmartPool.Instance.Spawn(player, Vector3.zero, Quaternion.identity);
        player.GetComponent<PlayerController>().InitInforPlayer(levelPlayer);
        EXPNEED = PlayerController.instance.expNeed;
    }
    public void PlayerUpLevel(int level)
    {
        PlayerController.instance.InitInforPlayer(level);
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
                Time.timeScale = 0;
            }
            else
            {
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
        if(currentEnemy >= 15)
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
    private void OnPlayerDie()
    {
        Time.timeScale = 0;
    }
}
