using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public static Tile instance;
    #region 
    [SerializeField] protected SpriteRenderer Renderer;
    [SerializeField] private GameObject HighLight, outOfRangeHighlight;
    [SerializeField] private bool IsWalkable;
    [NonSerialized] public bool selected = false;
    public string tileName;
    [SerializeField] private LayerMask _layerMask;

    public BaseUnit occupiedUnit;
    //walkable is true if walkable is true and there is no unit on the tile (occupied unit == null)
    public bool walkable => IsWalkable && occupiedUnit == null;
    #endregion
    private void Awake() => instance = this;
    public virtual void Init(int x, int y)
    {
        //if we are offset, set it to offset color. else set it to the base color
    }

    private void OnMouseEnter()
    {
        var hero = UnitManager.Instance.selectedHero;
        var enemy = UnitManager.Instance.selectedEnemy;
        if (hero != null)
        {
            if (!hero.inRange(transform.position))
            {
                outOfRangeHighlight.SetActive(true);
            }
            else
            {
                HighLight.SetActive(true);
            }
        }
        else
        {
            HighLight.SetActive(true);
        }

        MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        outOfRangeHighlight.SetActive(false);
        HighLight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    public void OnMouseDown()
    {
        //if its not the heroes turn yet, the script wont allow the player to click tiles
        if (GameManager.Instance.gameState != GameState.HeroesTurn) return;

        if (occupiedUnit != null)
        {
            //if selected tile is a hero. assign the selected hero to selected
            if (occupiedUnit.faction == Faction.Hero)
            {
                UnitManager.Instance.SetSelectedHero((BaseHero)occupiedUnit);
            }
            else
            {
                //if the next selected tile is not a hero its an enemy
                if (UnitManager.Instance.selectedHero != null)
                {
                    //get the instance of the enemy on the selected tile
                    var enemy = (BaseEnemy)occupiedUnit;
                    //get the instance of the previously selected hero
                    var hero = UnitManager.Instance.selectedHero;
                    //run the attack script on the hero
                    hero.Attack(enemy, transform.position);

                    //deselect the hero;
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            //if the next tile we've selected after selecting a tile is not a hero nor an enemy.
            //move the hero that tile
            if (UnitManager.Instance.selectedHero != null)
            {
                SetHero(UnitManager.Instance.selectedHero);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        //this function makes the tile, occupied by a unity (enemy or hero). this means nothing can spawn there
        //if the tile selected is occupied, it'll not set anything to that tile
        if (unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.occupiedTile = this;
    }

    public void SetHero(BaseUnit unit)
    {
        var lastPos = unit.transform.position;
        var distanceTraveledX = Mathf.Abs(lastPos.x - transform.position.x);
        var distanceTraveledY = Mathf.Abs(lastPos.y - transform.position.y);
        //this function makes the tile, occupied by a unit (enemy or hero). this means nothing can spawn there
        //if the tile selected is occupied, it'll not set anything to that tile
        if (unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        //if the distance between the tile you selected and the range your player can travel is too big
        // the player wont walk
        if (distanceTraveledX < unit.moveRange && distanceTraveledY < unit.moveRange)
        {
            if (walkable && !unit.moved)
            {
                UnitManager.Instance.SetSelectedHero(null);
                unit.transform.position = transform.position;
                occupiedUnit = unit;
                unit.occupiedTile = this;

                //set moved to true so the hero cannot move anymore
                unit.moved = true;
            }
        }
    }

    public void SetEnemy(BaseUnit unit)
    {
        //this function makes the tile, occupied by a unit (enemy or hero). this means nothing can spawn there
        //if the tile selected is occupied, it'll not set anything to that tile
        if (unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.occupiedTile.selected = false;

        //if the distance between the tile you selected and the range your player can travel is too big
        // the player wont walk

        if (walkable && !unit.moved)
        {
            UnitManager.Instance.SetSelectedEnemy(null);
            unit.transform.position = transform.position;
            occupiedUnit = unit;
            unit.occupiedTile = this;

            //set moved to true so the hero cannot move anymore
            unit.moved = true;
            //print("player can spawn here");
        }

    }


}
