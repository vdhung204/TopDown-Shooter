using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpConfig : SerializedScriptableObject
{
    [NonSerialized, OdinSerialize]
    public Dictionary<int, LevelPlayer> ExpPlayerMap = new Dictionary<int, LevelPlayer>();

    private LevelPlayer[] expPlayer;

    public void ConvertDataExpPlayer()
    {
        ExpPlayerMap = new Dictionary<int, LevelPlayer>();
        foreach (var entry in expPlayer)
        {
            ExpPlayerMap.Add(entry.level, entry);
        }
    }
    public LevelPlayer GetExpPlayerByLevel(int level)
    {
        if (ExpPlayerMap.TryGetValue(level, out var dataCsv))
        {
            return dataCsv;
        }
        return ExpPlayerMap[0];
    }
}
[Serializable]
public struct LevelPlayer
{
    public int level;
    public int expNeed;
}