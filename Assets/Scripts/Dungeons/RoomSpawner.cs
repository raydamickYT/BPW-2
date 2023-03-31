using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public static RoomSpawner instance;
    #region  variables and regions
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    private LevelGenerator Templates;

    private int Rand;
    public bool spawned = false;

    public float waitTime = 4f;

    //grid spawner

    #endregion

    private void Awake()
    {
        instance = this;
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGenerator>();
    }

    public void Start()
    {
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.1f);
    }

    public void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                Rand = Random.Range(0, Templates.bottomRooms.Length);
                Instantiate(Templates.bottomRooms[Rand], transform.position, Templates.bottomRooms[Rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                Rand = Random.Range(0, Templates.topRooms.Length);
                Instantiate(Templates.topRooms[Rand], transform.position, Templates.topRooms[Rand].transform.rotation);
                //grid wordt gemaakt voor iedere room. positie van de room wordt meegegeven aan de functie.
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                Rand = Random.Range(0, Templates.leftRooms.Length);
                if (Rand == 0)
                {
//                    print("this is empty");
                }
                else
                {

                    Instantiate(Templates.leftRooms[Rand], transform.position, Templates.leftRooms[Rand].transform.rotation);
                }
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                Rand = Random.Range(0, Templates.rightRooms.Length);
                Instantiate(Templates.rightRooms[Rand], transform.position, Templates.rightRooms[Rand].transform.rotation);
            }
            spawned = true;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {//checking if there is any rooms with door leading to nothing
         // if there is a null error, some spawnpoints are colliding with the "Destroyer" point. just add the roomspawner script to the Destroyer.
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            { //walls blocking off any openings
                Instantiate(Templates.closedRoom, transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }
}
