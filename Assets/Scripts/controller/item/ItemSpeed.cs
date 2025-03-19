using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : BaseItem
{
    protected override void ItemEffect(GameObject target)
    {
        target.AddComponent<ItemEffectSpeed>().SpeedUp();
    }
}
