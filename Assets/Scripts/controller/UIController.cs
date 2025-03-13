using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image imgFillHpBar;
    public Image imgFillExpBar;

    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public void FillHpPlayer(float hp, float maxHp)
    {
        imgFillHpBar.fillAmount = hp / maxHp;
    }
    public void FillExpPlayer(int exp, int maxExp)
    {
        imgFillExpBar.fillAmount = (float)exp / maxExp;
    }
}
