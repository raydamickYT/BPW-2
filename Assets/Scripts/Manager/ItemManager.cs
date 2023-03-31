using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    private List<ScriptableUnit> _units;

    public List<ScriptableItems> items = new List<ScriptableItems>();
    public Transform itemContent;
    public GameObject inventoryItem;
    public ItemController[] inventoryItems;

    private void Awake()
    {
        instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("SCriptableUnits").ToList();
    }
    public void spawnItem(Tile enemyTile)
    {
        var randomPrefab = GetRandomItem<BaseItem>(Faction.Items);
        var spawnedItem = Instantiate(randomPrefab);
        var randomSpawnTile = enemyTile;

        randomSpawnTile.SetItem(spawnedItem);
    }
    private T GetRandomItem<T>(Faction faction) where T : BaseItem
    {
        return (T)_units.Where(u => u.faction == faction).OrderBy(o => Random.value).First().ItemPrefab;
    }

    public void Add(ScriptableItems item)
    {
        items.Add(item);
    }
    public void Remove(ScriptableItems item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        //delete item gameobject zodat hij niet meerdere keren in de inventory kan spawnen
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        //voeg opgepakte items aan de inventory toe
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            //kijkt naar de objecten in de prefab "item"
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        setInventoryItems();
    }

    public void setInventoryItems(){
        inventoryItems = itemContent.GetComponentsInChildren<ItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].addItem(items[i]);
        }
    }
}
