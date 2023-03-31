using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTutorial : MonoBehaviour
{
    public GameObject start;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BaseHero>() != null)
        {
            if (other.GetComponent<BaseHero>().currentRoom.GetComponent<AddRoom>().enemiesInRoom.Count == 0)
            {
                start.SetActive(true);
                print("youre done!");
            }
            else
            {
                print("finish off all the enemies first!!");
            }
        }
    }


}
