using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<BaseItem>() != null){
            ItemManager.instance.Add(other.GetComponent<BaseItem>().item);
            Destroy(other.gameObject);
        }
    }
}
