using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseHero selectedHero;
    public BaseEnemy selectedEnemy;

    private void Awake()
    {
        Instance = this;

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
}
