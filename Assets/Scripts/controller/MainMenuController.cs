using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button btnSetting;
    public Button btnBattle;
    public Text txtGem;
    public Text txtCoin;
    public Button btnShop;
    public GameObject popupSetting;
    public GameObject popupShop;
    public GameObject popupChangeCharater;

    private void Awake()
    {
        btnSetting.onClick.AddListener(OnSettingClick);
        btnShop.onClick.AddListener(OnShopClick);
        btnBattle.onClick.AddListener(OnBattleClick);
    }
    void OnSettingClick()
    {
        popupSetting.SetActive(true);
    }
    void OnShopClick()
    {
        popupShop.SetActive(true);
    }
    void OnBattleClick()
    { 
        popupChangeCharater.SetActive(true);
    }
}
public enum SceneName
{
    MainMenu,
    GamePlayScene,
    Loading,
}
