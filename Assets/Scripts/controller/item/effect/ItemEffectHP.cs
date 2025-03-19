using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectHP : MonoBehaviour
{
    public void AddLive()
    {
        this.gameObject.GetComponent<PlayerController>().UpHp();
    }
}
