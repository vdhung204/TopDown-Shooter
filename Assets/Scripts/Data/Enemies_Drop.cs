using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Drop : SerializedScriptableObject
{

    [NonSerialized, OdinSerialize]
    public Dictionary<int, Drop> expEnemiesMap = new Dictionary<int, Drop>();

    private Drop[] expEnemies;

    public void ConvertDataExpEnemies()
    {
        expEnemiesMap = new Dictionary<int, Drop>();
        foreach (var entry in expEnemies)
        {
            expEnemiesMap.Add(entry.level, entry);
        }
    }
    public Drop GetExpEnemiesByLevel(int level)
    {
        if (expEnemiesMap.TryGetValue(level, out var dataCsv))
        {
            return dataCsv;
        }
        return expEnemiesMap[0];
    }
}

[Serializable]
public struct Drop
{
    public int level;
    public int expDrop;
}
