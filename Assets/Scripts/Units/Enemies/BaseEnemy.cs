using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseEnemy : BaseUnit
{
    public static BaseEnemy instance;
    [NonSerialized] public bool found = false;

    private void Awake()
    {
        instance = this;
    }

    public void EnemyTurn(Tile enemyTile, BaseEnemy enemy)
    {
        UnitManager.Instance.SetSelectedEnemy((BaseEnemy)enemyTile.occupiedUnit);
        var walkTile = LevelGenerator.Instance.GetEnemyWalkTile(moveRange, transform);
        walkTile.SetEnemy(UnitManager.Instance.selectedEnemy);

        //enemy.enemyAttack();
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
