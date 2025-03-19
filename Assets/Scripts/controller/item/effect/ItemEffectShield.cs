using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectShield : MonoBehaviour
{
    public void Shield()
    {
        this.gameObject.GetComponent<PlayerController>().Shield();
    }
}
