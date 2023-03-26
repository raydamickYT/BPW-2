using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    private List<BaseEnemy> _selectedEnemies;
    public BaseHero selectedHero;
    public BaseEnemy selectedEnemy;

    private void Awake()
    {
        Instance = this;
        _selectedEnemies = new List<BaseEnemy>();
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }
    public void SpawnHeroes()
    {
        var heroCount = 1;
        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = LevelGenerator.Instance.GetHeroSpawnTile();

            randomSpawnTile.SetUnit(spawnedHero);
        }
        GameManager.Instance.UpdateGameState(GameState.HeroesTurn);
    }
    public void SpawnEnemies()
    {
        var enemyCount = 1;
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = LevelGenerator.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.faction == faction).OrderBy(o => Random.value).First().unitPrefab;
    }

    public void SetSelectedHero(BaseHero Hero)
    {
        selectedHero = Hero;
        MenuManager.Instance.ShowSelectedHero(Hero);
    }


    public void SetSelectedEnemy(BaseEnemy Enemy)
    {
        selectedEnemy = Enemy;
        MenuManager.Instance.ShowSelectedEnemy(Enemy);
    }

    public void FindEnemy()
    {
        var GetEnemyTile = LevelGenerator.Instance.FindEnemy();
        var enemy = GetEnemyTile.occupiedUnit.GetComponent<BaseEnemy>();
        _selectedEnemies.Add(enemy);

        for (int i = 0; i < _selectedEnemies.Count; i++)
        {
            _selectedEnemies[i].GetComponent<BaseEnemy>().test();
        }
        enemy.moved = false;
        if (GetEnemyTile.occupiedUnit.faction == Faction.Enemy)
        {
            enemy.EnemyTurn(GetEnemyTile, enemy);
        }
        _selectedEnemies.Clear();
        print("list count "+_selectedEnemies.Count);
    }
}
