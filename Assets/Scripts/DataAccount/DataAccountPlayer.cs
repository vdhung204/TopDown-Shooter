using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAccountPlayer
{
    private static PlayerInfor _playerInfor;
    private static PlayerSetting _playerSetting;

    public static PlayerInfor PlayerInfor
    {
        get
        {
            if (_playerInfor != null)
                return _playerInfor;

            var local = PlayerPrefs.GetString(DataAccountPlayerConstans.PLAYER_INFOR);

            if (!string.IsNullOrEmpty(local))
            {
               _playerInfor = JsonConvert.DeserializeObject<PlayerInfor>(local);
            }
            else
            {
                _playerInfor = new PlayerInfor();
            }

            return _playerInfor;
        }
    }
    public static PlayerSetting PlayerSetting
    {
        get
        {
            if (_playerSetting != null)
                return _playerSetting;

            var local = PlayerPrefs.GetString(DataAccountPlayerConstans.PLAYER_SETTING);

            if (!string.IsNullOrEmpty(local))
            {
                _playerSetting = JsonConvert.DeserializeObject<PlayerSetting>(local);
            }
            else
            {
                _playerSetting = new PlayerSetting();
            }

            return _playerSetting;
        }
    }


    public static void SaveDataPlayerInfor()
    {
        PlayerPrefs.SetString(DataAccountPlayerConstans.PLAYER_INFOR, JsonConvert.SerializeObject(_playerInfor));
    }
    public static void SaveDataPlayerSetting()
    {
        PlayerPrefs.SetString(DataAccountPlayerConstans.PLAYER_SETTING, JsonConvert.SerializeObject(_playerSetting));
    }
}