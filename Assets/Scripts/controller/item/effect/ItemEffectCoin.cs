using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectCoin : MonoBehaviour
{
    public void TakeCoin()
    {
        this.gameObject.GetComponent<PlayerController>().TakeCoin(); 
    }
}
