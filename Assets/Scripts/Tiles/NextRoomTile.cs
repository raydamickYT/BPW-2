using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomTile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private Vector2 jumpDistance;
    private void OnMouseEnter() {
        _highlight.SetActive(true);
    }
    private void OnMouseDown() {
        BaseHero.instance.MoveRooms(jumpDistance);
    }

    private void OnMouseExit() {
        _highlight.SetActive(false);
    }
}
