using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public Button btnRePlay;
    public Button btnExit;
    public Button btnResume;
    public static PauseController instance { private set; get; }
    private void Awake()
    {
        instance = this;
        btnRePlay.onClick.AddListener(OnRePlay);
        btnExit.onClick.AddListener(OnExit);
        btnResume.onClick.AddListener(OnResume);
    }
    private void OnRePlay()
    {
        GameManager.instance.RePlay();
    }
    private void OnExit()
    {
        GameManager.instance.Exit();
    }
    private void OnResume()
    {
        GameManager.instance.ResumeGame();
    }
}
