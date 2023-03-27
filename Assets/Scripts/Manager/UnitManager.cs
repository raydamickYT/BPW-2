using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private float enemCount = 1;
    private Tile _enemyTile;
    private BaseEnemy _enemy;
    private List<ScriptableUnit> _units;
    private List<BaseEnemy> _selectedEnemies;
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
        _selectedEnemies = new List<BaseEnemy>();
        for (int i = 0; i < enemCount; i++)
        {
            _enemyTile = LevelGenerator.Instance.FindEnemy();
            _enemyTile.selected = true;
            _enemy = _enemyTile.occupiedUnit.GetComponent<BaseEnemy>();
            _selectedEnemies.Add(_enemy);
        }

        for (int i = 0; i < _selectedEnemies.Count; i++)
        {
            _selectedEnemies[i].moved = false;
            _selectedEnemies[i].EnemyTurn(_selectedEnemies[i].occupiedTile, _selectedEnemies[i]);
            //geef de tile waar de enemy op staat mee en de enemy zelf.
        }
    }
}
