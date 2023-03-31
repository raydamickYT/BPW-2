using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BaseHero>() != null)
        {
            if (other.GetComponent<BaseHero>().currentRoom.GetComponent<AddRoom>().enemiesInRoom.Count == 0)
            {
                print("youre done!");
            } else {
                print("finish off all the enemies first!!");
            }
        }
    }

}
