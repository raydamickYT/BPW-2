using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public static AddRoom instance;

    private LevelGenerator templates;

    private void Awake()
    {
        instance = this;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelGenerator>();
        templates.rooms.Add(this.gameObject);
      }

    private void Start()
    {
        //when room is created, generate a grid that fits the room
        LevelGenerator.Instance.GenerateGrid(this.gameObject.transform.position);
    }
}
