using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableItem")]
public class ScriptableItems : ScriptableObject
{
    public Faction faction;
    public ItemType itemType;
    public BaseItem itemPrefab;
    public Sprite icon;
    public int id;
    public float value;
    public string itemName;


}
public enum ItemType{
    Healing = 0 ,
    DmgIncrease = 1,
    EmptyItem = 2,
}
