using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirmation : MonoBehaviour
{
    public void move(){
        //als de player ja zegt
        BaseHero.instance.MoveRooms(BaseHero.instance.jumpDistance);
        MenuManager.Instance.hideConfirmation();
    }

    public void dontMove(){
        //als de player nee zegt
        MenuManager.Instance.hideConfirmation();
    }
}
