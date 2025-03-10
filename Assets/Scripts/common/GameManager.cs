using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemies;
    public List<Transform> enemiesPos;
    public GameObject player;
    public SpriteRenderer spriteRenderer;
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
    }
    private void Start()
    {
        SpawnPlayer();
       
        this.RegisterListener(EventID.PlayerUpEXP, (sender, param) => ChangeEXPPlayer((int)param));
        this.RegisterListener(EventID.PlayerUpLevel, (sender, param) => PlayerUpLevel((int)param));
        this.RegisterListener(EventID.OutOffEnemy, (sender, param) => SetCountEnemyWave((int)param));
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
            StartToSpawnEnemies();*/
        CheckToSpawnEnemies();


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
            var timeDelay = waveSpawn.GetWaveInfor(levelConfig, currentWave).delay_time;
            this.PostEvent(EventID.WaveEnd, timeDelay);
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
            yield return new WaitForSeconds(5f);
            StartToSpawnEnemies();
            StartCoroutine(SpawnEnemies());
        }
    }
    private IEnumerator SpawnWave(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SpawnEnemies());
    }
    private void CheckToSpawnEnemies()
    {
        var waveEnemies = waveSpawn.GetLevelConfig(levelConfig);
        //round.text = $"{currentWave}/{waveSpawn.GetLevelConfig(levelConfig).waveEnemy.Length}";
        countEnemy = waveSpawn.GetWaveInfor(levelConfig, currentWave).spawn_enemy;
        for (int i = 0; i < waveEnemies.waveEnemy.Length; i++)
        {
            if (currentWave == waveEnemies.waveEnemy[i].wave)
            {
                this.PostEvent(EventID.CountEnemy, waveEnemies.waveEnemy[i].spawn_enemy);
                StartCoroutine(SpawnWave(waveEnemies.waveEnemy[i].delay_time));

                return;
            }

        }
    }
    public Vector3 GetSizeBg()
    {

        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size;
        }
        return Vector2.zero;


    }
    public void StartToSpawnEnemies()
    {
        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("No enemies to spawn.");
            return;
        }

        int enemyIndex = Random.Range(0, enemies.Count);
        GameObject enemyPrefab = enemies[enemyIndex];
        var bgSize = GetSizeBg();
        float xLimit = bgSize.x / 2 - 1f;
        float yLimit = bgSize.y / 2 - 1f;
        Vector3 spawnPos = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0);
        var enemyClone = SmartPool.Instance.Spawn(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesPos.Add(enemyClone.transform);
    }
}
