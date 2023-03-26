using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryDungeon : MonoBehaviour
{
    public static DictionaryDungeon instance;
    private Dictionary<Vector2, Tile> Tiles;

    private void Awake()
    {
        instance = this;
    }
    public void AddToDictionary(int x, int y, Tile spawnedTile)
    {

        Tiles = new Dictionary<Vector2, Tile>();
        Tiles.Add(new Vector2(x, y), spawnedTile);

    }
}
