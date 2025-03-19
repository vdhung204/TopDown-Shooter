using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : BaseItem
{
    protected override void ItemEffect(GameObject target)
    {
        target.AddComponent<ItemEffectShield>().Shield();
    }
}
