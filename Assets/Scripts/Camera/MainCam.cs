using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public static MainCam instance;
    void Awake() => instance = this;

    public void MoveCamPos(Vector2 levelPos){
        var temp = new Vector3(levelPos.x,levelPos.y, transform.position.z);
        transform.position = temp;
    }
}
