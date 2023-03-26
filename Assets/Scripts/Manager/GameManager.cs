using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState gameState;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                BaseHero.instance.StartHeroTurn();
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.FindEnemy();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}
