using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    #region Variables
    [SerializeField] private int Width, Height;
    [SerializeField] private float offset;
    [SerializeField] private Tile _GrassTile, _MountainTile;
    //[SerializeField] private Transform Cam;
    private Dictionary<Vector2, Tile> Tiles;

    #endregion
    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        Tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? _MountainTile : _GrassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x + transform.position.x - offset, y + transform.position.y - offset), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init(x, y);
                Tiles.Add(new Vector2(x, y), spawnedTile);

            }
        }

        GameManager.Instance.UpdateGameState(GameState.SpawnEnemies);
    }
}
