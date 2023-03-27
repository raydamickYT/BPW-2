using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;
    #region RoomSpawner
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;


    void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    GameManager.Instance.UpdateGameState(GameState.SpawnHeroes);
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    #endregion
    #region gridspawner
    [SerializeField] private int Width, Height;
    [SerializeField] private float offset;
    [SerializeField] private Tile _GrassTile, _MountainTile;
    //[SerializeField] private Transform Cam;

    private Dictionary<Vector2, Tile> Tiles;
    public Dictionary<Vector2, Tile> Opslag;

    private void Awake()
    {
        Instance = this;
        Opslag = new Dictionary<Vector2, Tile>();
    }

    public void GenerateGrid(Vector2 pos)
    {
        Tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? _MountainTile : _GrassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3((x + transform.position.x + pos.x) + offset, (y + transform.position.y + pos.y) + offset), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init(x, y);
                AddToList(spawnedTile);
                Tiles.Add(new Vector2(x, y), spawnedTile);
            }
        }
        //Cam.transform.position = new Vector3((float)Width / 2 - 0.5f, (float)Height / 2 - 0.5f, -10);

        GameManager.Instance.UpdateGameState(GameState.SpawnEnemies);
    }


    private void AddToList(Tile NewTile)
    {
        //because the dictionary resets every time the function is called. this seperate function will keep track of all the tiles added
        int x = 0;
        int y = 0;
        Opslag.Add(new Vector2(NewTile.transform.position.x + x++, NewTile.transform.position.y + y++), NewTile);
    }
    public Tile GetHeroSpawnTile()
    {
        var currentRoom = BaseHero.instance.currentRoom;
        float currentRoomMinX = currentRoom.transform.position.x - 4f;
        float currentRoomMaxX = currentRoom.transform.position.x;
        float currentRoomMinY = currentRoom.transform.position.y - 4f;
        float currentRoomMaxY = currentRoom.transform.position.y + 4;
        //er word uit de dictionary "Opslag" een Tile gehaald die tussen de waardes van het start vlak zitten.
        return Opslag.Where(t => t.Key.x < currentRoomMaxX && t.Key.x > currentRoomMinX && t.Key.y > currentRoomMinY && t.Key.y < currentRoomMaxY && t.Value.walkable).OrderBy(t => Random.value).First().Value;
        //return Opslag.Where(t => t.Key.x < 3.5 && t.Key.x > 1 && t.Key.y > 1 && t.Key.y < 8.6 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile FindEnemy()
    {
        //script will find parametres for current room and checks within those bounds if there is an enemy unity in there.
        var currentRoom = BaseHero.instance.currentRoom;
        float currentRoomMinX = currentRoom.transform.position.x - 4f;
        float currentRoomMaxX = currentRoom.transform.position.x + 4f;
        float currentRoomMinY = currentRoom.transform.position.y - 4f;
        float currentRoomMaxY = currentRoom.transform.position.y + 4;

        var occupied = Tile.instance.occupiedUnit;
        var enemyPos = Opslag.Where(t => t.Key.x < currentRoomMaxX && t.Key.x > currentRoomMinX && t.Key.y > currentRoomMinY && t.Key.y < currentRoomMaxY && t.Value.occupiedUnit != null && t.Value.occupiedUnit.faction == Faction.Enemy).First().Value;

        //var enemyPos = Opslag.Where(t => t.Key.x < 9 && t.Key.x > 1 && t.Key.y > 1 && t.Key.y < 9 && t.Value.occupiedUnit != null && t.Value.occupiedUnit.faction == Faction.Enemy).First().Value;

        return enemyPos;
    }
    public Tile GetEnemyWalkTile(float walkRange, Transform enemPos)
    {
        float currentRoomMinX = enemPos.position.x - walkRange;
        float currentRoomMaxX = enemPos.position.x + walkRange;
        float currentRoomMinY = enemPos.position.y - walkRange;
        float currentRoomMaxY = enemPos.position.y + walkRange;

        var enemyPos = Opslag.Where(t => t.Key.x < currentRoomMaxX && t.Key.x > currentRoomMinX && t.Key.y > currentRoomMinY && t.Key.y < currentRoomMaxY && t.Value.walkable && !t.Value.selected).OrderBy(t => Random.value).First().Value;
        return enemyPos;

        //var enemyPos = Opslag.Where(t => t.Key.x < currentRoomMaxX && t.Key.x > currentRoomMinX && t.Key.y > currentRoomMinY && t.Key.y < currentRoomMaxY && t.Value.walkable).OrderBy(t => Random.value).First().Value;
        //return enemyPos;
    }

    public Tile GetEnemySpawnTile()
    {
        return Tiles.Where(t => t.Key.x > Width / 2 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    #endregion
}
