using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : BaseItem
{
    protected override void ItemEffect(GameObject target)
    {
        target.AddComponent<ItemEffectCoin>().TakeCoin();
        //this.PostEvent(EventID.AddHP);
    }


}
