using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP : BaseItem
{
    protected override void ItemEffect(GameObject target)
    {
        target.AddComponent<ItemEffectHP>().AddLive();
    }
}
