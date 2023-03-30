using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endTurn : MonoBehaviour
{
    public GameObject box;

    public void OnButtonClick()
    {
        if (GameManager.Instance.gameState == GameState.HeroesTurn)
        {
            if (BaseHero.instance.currentRoom.GetComponent<AddRoom>().enemiesInRoom.Count == 0)
            {
                GameManager.Instance.UpdateGameState(GameState.HeroesTurn);
                UnitManager.Instance.SetSelectedHero(null);
            }else{
                GameManager.Instance.UpdateGameState(GameState.EnemiesTurn);
            }
        }
    }
}
