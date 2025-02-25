using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemies;
    public List<Transform> enemiesPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SpawnEnemies();
        
    }
    public void SpawnEnemies()
    {
        // Ensure there are enemies to spawn
        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogWarning("No enemies to spawn.");
            return;
        }

        int enemyIndex = Random.Range(0, enemies.Count);
        GameObject enemyPrefab = enemies[enemyIndex];

        var enemyClone = SmartPool.Instance.Spawn(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemiesPos.Add(enemyClone.transform);
    }
}
