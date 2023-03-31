using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public float enemCount = 1;
    private Tile _enemyTile;
    private BaseEnemy _enemy;
    private List<ScriptableUnit> _units;
    public BaseHero selectedHero;
    public BaseEnemy selectedEnemy;

    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("SCriptableUnits").ToList();
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
        for (int i = 0; i < enemCount; i++)
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
    private T GetRandomItem<T>(Faction faction) where T : BaseItem
    {
        return (T)_units.Where(u => u.faction == faction).OrderBy(o => Random.value).First().ItemPrefab;
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
        var enemiesInCurrentRoom = BaseHero.instance.currentRoom.GetComponent<AddRoom>().enemiesInRoom;
        for (int i = 0; i < enemiesInCurrentRoom.Count; i++)
        {
            enemiesInCurrentRoom[i].moved = false;
            //get the enemies to move
            enemiesInCurrentRoom[i].EnemyTurn(enemiesInCurrentRoom[i].occupiedTile, enemiesInCurrentRoom[i]);
        }
    }
}
