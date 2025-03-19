using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameControlelr : MonoBehaviour
{
    public Text txtEndGame;
    public Button btnRePlay;
    public Button btnExit;
    public Text txtScore;
    public Text txtGold;

    public static EndGameControlelr instance { private set; get; }

    private void Awake()
    {
        instance = this;
        btnRePlay.onClick.AddListener(OnRePlay);
        btnExit.onClick.AddListener(OnExit);
    }
    private void OnRePlay()
    {
        GameManager.instance.RePlay();
    }
    private void OnExit()
    {
        GameManager.instance.Exit();
    }
    public void ShowEndGame(bool isWin, int score, int gold)
    {
        txtEndGame.text = isWin ? "VICTORY" : "Lose";
        txtEndGame.color = isWin ? Color.yellow : Color.grey;
        txtScore.text = $"{score}";
        txtGold.text = $"{gold}";
    }
}
