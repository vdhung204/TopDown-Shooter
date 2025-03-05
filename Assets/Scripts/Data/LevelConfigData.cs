using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfigData : ScriptableObject
{
    public LevelConfig[] levelConfigs;

    public WaveInfor GetWaveInfor(int lv, int waveId)
    {
        var lvConfig = GetLevelConfig(lv);

        var waveEnemy = lvConfig.waveEnemy;

        foreach (WaveInfor wave in waveEnemy)
        {
            if (wave.wave == waveId)
                return wave;
        }

        return waveEnemy[0];
    }

    public LevelConfig GetLevelConfig(int lv)
    {
        foreach (var lvConfig in levelConfigs)
        {
            if (lvConfig.level == lv)
                return lvConfig;
        }

        return levelConfigs[0];
    }
}
[Serializable]
public struct WaveInfor
{
    public int wave;
    public int delay_time;
    public int spawn_enemy;
}

[Serializable]
public struct LevelConfig
{
    public int level;
    public WaveInfor[] waveEnemy;
}

