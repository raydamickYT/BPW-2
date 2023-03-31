using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNewRoom : MonoBehaviour
{
    public GameObject door;
    public Vector2 jumpDistance;

    public void ifUnlocked()
    {
        if (GetComponentInParent<AddRoom>().unlocked)
        {
            if (door != null)
                door.SetActive(false);
            else
            {
                print("door is already unlocked");
            }
        }
    }

}
