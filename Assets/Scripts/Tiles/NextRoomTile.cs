using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomTile : MonoBehaviour
{
    [SerializeField] private GameObject HighLight;
    private void OnMouseEnter() {
        HighLight.SetActive(true);
    }
}
