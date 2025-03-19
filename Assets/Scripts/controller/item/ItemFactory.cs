using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemFactory : SingletonMono<ItemFactory>
{
    public GameObject prefabItem;
    public ItemHP prefabItemHP;
    public ItemCoin prefabItemCoin;
    public ItemShield prefabItemShield;
    public ItemSpeed prefabItemSpeed;
    public static List<BaseItem> listItems = new List<BaseItem>();

    public static void CheckItem(Transform target)
    {
        if(target == null)
            return;
        foreach (BaseItem item in listItems)
        {
            if (Vector3.Distance(target.position, item.transform.position) < 2f)
            {
                item.setTarget(target);
                listItems.Remove(item);
                break;
            }
        }
    }



    public BaseItem Create(int index, Vector3 pos)
    {
        switch (index)
        {
            case 0:
                return CreatItemHp(pos);
            case 1:
                return CreatItemCoin(pos);
            case 2:
                return CreatItemCoin(pos);
            case 3:
                return CreatItemHp(pos);
            case 4:
                return CreatItemHp(pos);
            case 5:
                return CreatItemShield(pos);
            case 6: 
                return CreatItemSpeed(pos);
            case 7:
                return CreatItemCoin(pos);
            case 8:
                return CreatItemSpeed(pos);
            case 9:
                return CreatItemShield(pos);
            case 10:
                return CreatItemCoin(pos);

        }
        return null;
    }

    private ItemHP CreatItemHp(Vector3 pos)
    {
        ItemHP item = Instantiate(prefabItemHP, pos, prefabItemHP.transform.rotation);
        listItems.Add(item);
        return item;
    }

    private ItemCoin CreatItemCoin(Vector3 pos)
    {
        ItemCoin item = Instantiate(prefabItemCoin, pos, prefabItemCoin.transform.rotation);
        listItems.Add(item);
        return item;
    }
    private ItemShield CreatItemShield(Vector3 pos)
    {
        ItemShield item = Instantiate(prefabItemShield, pos, prefabItemShield.transform.rotation);
        listItems.Add(item);
        return item;
    }
    private ItemSpeed CreatItemSpeed(Vector3 pos)
    {
        ItemSpeed item = Instantiate(prefabItemSpeed, pos, prefabItemSpeed.transform.rotation);
        listItems.Add(item);
        return item;
    }

}
