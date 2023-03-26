using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public static BaseEnemy instance;

    private void Awake()
    {
        instance = this;
    }

    public void EnemyTurn()
    {
        var GetEnemyTile = LevelGenerator.Instance.FindEnemy();
        var enemy = GetEnemyTile.occupiedUnit.GetComponent<BaseEnemy>();
        enemy.moved = false;
        if (GetEnemyTile.occupiedUnit.faction == Faction.Enemy)
        {
            UnitManager.Instance.SetSelectedEnemy((BaseEnemy)GetEnemyTile.occupiedUnit);
            var walkTile = LevelGenerator.Instance.GetEnemyWalkTile(moveRange);
                walkTile.SetEnemy(UnitManager.Instance.selectedEnemy);

            if (inRange(walkTile.transform.position))
            {
                print("found it");
            }
        }
        enemy.enemyAttack();
        GameManager.Instance.UpdateGameState(GameState.HeroesTurn);
    }

    public void enemyAttack()
    {
        var temp = FindObjectOfType<BaseHero>();
        var difference = temp.transform.position - transform.position;
        if (Mathf.Abs(difference.x) <= attackRange && Mathf.Abs(difference.y) <= attackRange)
        {
            temp.health -= attackDmg;
            print(temp.health);
            temp.Health();
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
