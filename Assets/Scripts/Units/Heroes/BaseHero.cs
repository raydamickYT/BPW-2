using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public static BaseHero instance;
    public GameObject currentRoom;
    public bool _movedRooms = false;
    public Vector2 jumpDistance = new Vector2(0, 0);

    private void Awake()
    {
        instance = this;
        currentHealth = health;
        //this'll determine where the player spawns.
        currentRoom = LevelGenerator.Instance.rooms[0];
    }
    public void StartHeroTurn()
    {
        //reset all bools at the start of the turn
        moved = false;
        currentRoom.gameObject.GetComponent<AddRoom>().unlocked = true;
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
    public void Health()
    {
        healthBar.fillAmount = (float)currentHealth / (float)health;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
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
    public void MoveRooms(Vector2 jumpDistance)
    {
        //print($"spawnpoint position {other.transform.position} and name {other.tag}");
        _movedRooms = true;
        moved = false;


        //moving the player
        Vector2 playerPos = transform.position;
        playerPos += jumpDistance;
        transform.position = playerPos;
        if (occupiedTile != null) occupiedTile.occupiedUnit = null;
        MainCam.instance.MoveCamPos(currentRoom.transform.position);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //print($"spawnpoint position {other.transform.position} and name {other.tag}");
        if (other.tag == "EntryPoints")
        {
            var NextRoom = other.GetComponentInParent<AddRoom>();
            if (currentRoom.GetComponent<AddRoom>().enemiesInRoom.Count == 0 || NextRoom.unlocked)
            {
                if (moved)
                {
                    MenuManager.Instance.showConfirmation();

                    //verander de current room naar de room waar we naartoe gaan.
                    currentRoom = LevelGenerator.Instance.rooms[NextRoom.GetCurrentRoom()];

                    //zeg dat deze doorgang officieel unlocked is
                    NextRoom.unlocked = true;

                    //zet de jump distance
                    jumpDistance = other.GetComponent<MoveToNewRoom>().jumpDistance;
                }
            }
            else
            {
                print("dood eerst alle enemies");
            }

        }

        //als we gemoved zijn dan moeten we de occupied tile veranderen.
        if (other.tag == "Tile" && _movedRooms)
        {
            if (other.GetComponent<Tile>() == null)
            {
                print("werkt niet");
            }
            else
            {
                other.GetComponent<Tile>().occupiedUnit = this;
                occupiedTile = other.GetComponent<Tile>();
                _movedRooms = false;
                moved = false;
            }
        }
    }

}
