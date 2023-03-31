using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ScriptableItems item;

    public void removeItem(){
        ItemManager.instance.Remove(item);

        Destroy(gameObject);
    }

    public void addItem(ScriptableItems newItem)
    {
        item = newItem;
    }


    //wordt gereferenced door een button in inventory
    public void useItem()
    {
        switch (item.itemType)
        {
            case ItemType.Healing:
                BaseHero.instance.increaseHealth(item.value);
                break;
            case ItemType.DmgIncrease:
                BaseHero.instance.increaseDmg(item.value, 3);
                break;
            case ItemType.EmptyItem:
            //do nothing
                break;
            default:
                break;
        }
        removeItem();
    }
}
