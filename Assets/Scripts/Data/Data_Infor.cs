using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class Data_Infor : SerializedScriptableObject
{
    [NonSerialized, OdinSerialize]
    public Dictionary<int, Data_object> LevelEnemyMap = new Dictionary<int, Data_object>();
    [NonSerialized, OdinSerialize]
    public Dictionary<int, Data_object> LevelPlayerMap = new Dictionary<int, Data_object>();

    private Data_object[] levelEnemy;
    private Data_object[] levelPlayer;

    public void ConvertDataLevelEnemy()
    {
        LevelEnemyMap = new Dictionary<int, Data_object>();

        foreach (var entry in levelEnemy)
        {
            LevelEnemyMap.Add(entry.level, entry);
        }
    }

    public void ConvertDataLevelPlayer()
    {
        LevelPlayerMap = new Dictionary<int, Data_object>();

        foreach (var entry in levelPlayer)
        {
            LevelPlayerMap.Add(entry.level, entry);
        }
    }

    public Data_object GetInforEnemiesByLevel(int level)
    {
        if (LevelEnemyMap.TryGetValue(level, out var dataCsv))
        {
            return dataCsv;
        }

        return LevelEnemyMap[0];
    }
    public Data_object GetInforPlayerByLevel(int level)
    {
        if (LevelPlayerMap.TryGetValue(level, out var dataCsv))
        {
            return dataCsv;
        }

        return LevelPlayerMap[0];

    }
}
[Serializable]
public struct Data_object
{
    public int level;
    public int damage;
    public int speed;
    public int hp;
}
