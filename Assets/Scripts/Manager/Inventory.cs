using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> PlayerInventory;

    private void Awake() {
        PlayerInventory = new List<BaseItem>();
    }
}
