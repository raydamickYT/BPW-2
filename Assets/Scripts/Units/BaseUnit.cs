using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    //reference naar scriptableunit.cs
    public Faction faction;
    public Image healthBar;
    public string unitName;
    public float health, moveRange, attackRange, attackDmg;
    [HideInInspector] public float currentHealth;
    public bool moved = false;

    private void Awake() {
        currentHealth = health;
    }
    private void UpdateCharacterUI()
    {
        healthBar.fillAmount = (float)currentHealth / (float)health;
    }


}
