using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    //reference naar scriptableunit.cs
    public Faction faction;
    public string unitName;
    public float health, moveRange, attackRange, attackDmg;
    public bool moved = false;

    public void Health()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
