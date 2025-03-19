using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectSpeed : MonoBehaviour
{
    public void SpeedUp()
    {
        this.gameObject.GetComponent<PlayerController>().SpeedUp(); 
    }
}
