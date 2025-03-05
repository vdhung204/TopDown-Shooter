using Core.Pool;
using System.Collections;
using System.Collections.Generic;
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
    private int countEnemis ;
    private int countEnemisWave =0 ;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        waveSpawn = Resources.Load<LevelConfigData>("LevelConfigData");
    }
    private void Start()
    {
        SpawnPlayer();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StartToSpawnEnemies();
        
    }
    void SpawnPlayer()
    {
        var players = SmartPool.Instance.Spawn(player, Vector3.zero, Quaternion.identity);
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
