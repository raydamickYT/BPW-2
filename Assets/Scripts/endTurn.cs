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
            GameManager.Instance.UpdateGameState(GameState.EnemiesTurn);
        }
    }
}
