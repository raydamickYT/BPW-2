using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public static BaseHero instance;
    public GameObject currentRoom;

    private void Awake()
    {
        instance = this;
        //this'll determine where the player spawns.
        currentRoom = LevelGenerator.Instance.rooms[0];
        //print("Current Room Name: " + currentRoom);
        //print("current room Transform: " + currentRoom.transform.position);
    }
    public void StartHeroTurn()
    {
        //reset all bools at the start of the turn
        moved = false;
    }

    public void Attack(BaseEnemy enemy, Vector2 enemyLocation)
    {
        var distanceX = Mathf.Abs(transform.position.x - enemyLocation.x);
        var distanceY = Mathf.Abs(transform.position.y - enemyLocation.y);

        if (distanceX < attackRange && distanceY < attackRange)
        {
            enemy.health -= attackDmg;
            enemy.Health();
        }
    }

    public bool inRange(Vector2 tilePos)
    {
        //if the distance between the tile the mouse hovers over and the player is more than the range it'll return false.
        var distanceTraveledX = Mathf.Abs(tilePos.x - transform.position.x);
        var distanceTraveledY = Mathf.Abs(tilePos.y - transform.position.y);
        if (distanceTraveledX < moveRange && distanceTraveledY < moveRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
