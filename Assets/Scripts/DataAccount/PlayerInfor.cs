using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor : MonoBehaviour
{
    public int coinPlayer;

    public int character = 1;
    //public int selectLevel = 1;
    public List<int> listCharacter = new List<int>() { 0 };
    // public HashSet<int> levelPlayerIsActived = new HashSet<int>() { 1 };


    public void ChangeCharacterId(int id)
    {
        character = id;
        DataAccountPlayer.SaveDataPlayerInfor();
    }
    public void ChangeLevelConfigPlayer(int level)
    {
        //selectLevel = level;
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
