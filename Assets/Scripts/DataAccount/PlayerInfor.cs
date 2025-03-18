using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor
{
    public double coinPlayer = 0;
    public double gem = 0;

    public int character = 0;
    //public int selectLevel = 1;
    public List<int> listCharacter = new List<int>() { 0 };
    // public HashSet<int> levelPlayerIsActived = new HashSet<int>() { 1 };


    public void ChangeCharacterId(int id)
    {
        character = id;
        DataAccountPlayer.SaveDataPlayerInfor();
    }
    public void BoughtCharacter(int id)
    {
        listCharacter.Add(id);
        DataAccountPlayer.SaveDataPlayerInfor();
    }
    public void ChangeLevelPlayer(int level)
    {
        //levelPlayerIsActived.Add(level);
        DataAccountPlayer.SaveDataPlayerInfor();
    }
}
