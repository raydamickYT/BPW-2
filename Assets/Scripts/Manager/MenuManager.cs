using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject SelectedHeroObject, TileInfoObject, TileUnitObject;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            //if the mouse is not hovering over a tile, dont show anything
            TileInfoObject.SetActive(false);
            TileUnitObject.SetActive(false);
            return;
        }
        //if the mouse hovers over a tile, it'll show the name of the tile and info
        TileInfoObject.GetComponentInChildren<Text>().text = tile.tileName;
        TileInfoObject.SetActive(true);

        //if there is a unit on the tile we hover our mouse over, it'll display the info of the unit
        if (tile.occupiedUnit)
        {
            TileUnitObject.GetComponentInChildren<Text>().text = tile.occupiedUnit.unitName;
            TileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero Hero)
    {
        if (Hero == null)
        {
            //if no hero is selected dont show anything
            SelectedHeroObject.SetActive(false);
            return;
        }

        //if hero is selected, show the name of the hero and the stats
        SelectedHeroObject.GetComponentInChildren<Text>().text = Hero.unitName;
        SelectedHeroObject.SetActive(true);
    }
        public void ShowSelectedEnemy(BaseEnemy Enemy)
    {
        if (Enemy == null)
        {
            //if no hero is selected dont show anything
            SelectedHeroObject.SetActive(false);
            return;
        }

        //if hero is selected, show the name of the hero and the stats
        SelectedHeroObject.GetComponentInChildren<Text>().text = Enemy.unitName;
        SelectedHeroObject.SetActive(true);
    }
}
