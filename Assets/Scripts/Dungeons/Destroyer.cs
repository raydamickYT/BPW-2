using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if its not deleting any player tiles
        if (!other.CompareTag("Tile") && !other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

}
