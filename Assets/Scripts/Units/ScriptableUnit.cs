using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableUnit")]
public class ScriptableUnit : ScriptableObject
{
   public Faction faction;
   public BaseUnit unitPrefab;
}

public enum Faction {
    Hero = 0,
    Enemy = 1,
    Items = 2
}