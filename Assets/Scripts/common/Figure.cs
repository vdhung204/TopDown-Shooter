using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Figure : MoveBase
{
    public float hp;
    public float damage;
    public GameObject popUpTextPrefab;
    public TMP_Text popUpText;
    public Transform efTakeDamage;
    public virtual void TakeDamage(float damage)
    {
        popUpText.text = damage.ToString();
        Instantiate(popUpTextPrefab.gameObject, efTakeDamage.position, Quaternion.identity);
        hp -= damage;
    }
}
