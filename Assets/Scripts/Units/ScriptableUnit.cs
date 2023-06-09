using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableUnit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction faction;
    public BaseUnit unitPrefab;
    public BaseItem ItemPrefab; 
    public Sprite icon;
    public int id;
    public float value;
    public string itemName;

}

public enum Faction
{
    Hero = 0,
    Enemy = 1,
    Items = 2
}