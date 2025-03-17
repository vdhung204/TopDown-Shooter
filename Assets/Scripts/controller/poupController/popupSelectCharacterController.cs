using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class popupSelectCharacterController : MonoBehaviour
{
    public Button btnPlay;
    public Button btnBackToMain;
    public Button btnBack;
    public Button btnNext;
    public Button btnSelect;
    public Image imgCharacter;
    public Sprite[] spCharacter;
    private int index;
    public GameObject poupSelect;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnClickBtnPlay);
        btnBackToMain.onClick.AddListener(OnClickBtnBackToMain);
        btnBack.onClick.AddListener(OnClickBtnBack);
        btnNext.onClick.AddListener(OnClickBtnNext);
        btnSelect.onClick.AddListener(OnClickBtnSelect);
    }
    private void Start()
    {
        imgCharacter.sprite = spCharacter[index];
        if (index <= 0)
        {
            btnBack.interactable = false;
        }
        else if (index >= spCharacter.Length - 1)
        {
            btnNext.interactable = false;
        }
    }
    void OnClickBtnPlay()
    {
        SceneManager.LoadScene(SceneName.GamePlayScene.ToString());
    }
    void OnClickBtnBackToMain()
    {
        poupSelect.gameObject.SetActive(false);
    }
    void OnClickBtnBack()
    {
        //index = System.Array.IndexOf(spCharacter, imgCharacter.sprite);
        index--;
        index = Mathf.Clamp(index, 0, spCharacter.Length - 1);
        if (index <= 0)
        {
            btnBack.interactable = false;
        }
        else
        {
            btnNext.interactable = true;
            
        }
        imgCharacter.sprite = spCharacter[index];
    }
    void OnClickBtnNext()
    {
        //index = System.Array.IndexOf(spCharacter, imgCharacter.sprite);
        index++;
        index = Mathf.Clamp(index, 0, spCharacter.Length - 1);
        if (index >= spCharacter.Length - 1)
        {
            btnNext.interactable = false;
        }
        else
        {
            btnBack.interactable = true;
        }
        imgCharacter.sprite = spCharacter[index];
    } void OnClickBtnSelect()
    {
        index = System.Array.IndexOf(spCharacter, imgCharacter.sprite);
        //DataAccountPlayer.PlayerInfor.ChangeCharacterId(index);
        Debug.Log("index: " + index);
        this.PostEvent(EventID.SelectCharacter, index);
    }
 }
