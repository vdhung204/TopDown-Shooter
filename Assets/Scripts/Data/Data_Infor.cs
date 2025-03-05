using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data_Infor : ScriptableObject
{

    public Data_object[] levelEnemy;

    public Data_object GetInforObjectByLevel(int level)
    {
        if (level >= levelEnemy.Length) return levelEnemy[0];

        foreach (var lvl in levelEnemy)
        {
            if (lvl.level == level) return lvl;
        }
        return levelEnemy[0];
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
