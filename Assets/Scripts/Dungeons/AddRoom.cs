using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AddRoom : MonoBehaviour
{
    public static AddRoom instance;
    [HideInInspector] public bool unlocked = false;

    private LevelGenerator templates;
    public List<BaseEnemy> enemiesInRoom;

    private void Awake()
    {
        instance = this;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGenerator>();
        templates.rooms.Add(this.gameObject);
        enemiesInRoom = new List<BaseEnemy>();
    }

    public int GetCurrentRoom()
    {
        var room = 0;
        for (int i = 0; i < templates.rooms.Count; i++)
        {
            var currentRoom = templates.rooms[i].transform.position;
            float currentRoomMinX = currentRoom.x - 4f;
            float currentRoomMaxX = currentRoom.x + 4f;
            float currentRoomMinY = currentRoom.y - 4f;
            float currentRoomMaxY = currentRoom.y + 4;
            if (transform.position.x < currentRoomMaxX && transform.position.x > currentRoomMinX && transform.position.y < currentRoomMaxY && transform.position.y > currentRoomMinY)
            {
                room = i;
                break;
            }
        }
        return room;
    }

    private void Start()
    {
        //when room is created, generate a grid that fits the room
        LevelGenerator.Instance.GenerateGrid(this.gameObject.transform.position);
    }

}
