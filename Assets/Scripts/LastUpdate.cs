using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastUpdate : MonoBehaviour
{
    //volgende keer doe ik dit deel in github

    // 3/6
     ///added player logic
    // if out of Moverange the player wont move to the tile and
    // if out of attack range the player wont attack
    // both are adjustable in the inspector for ease of use
    //was als laatste bezig in BaseEnemy om de enemy te laten bewegen 
    //(ga dit doen door de locaties van rooms en tile uit de lists/dictionary op te vragen)

    //20/3
    //in LevelGenerator vind je de enemy in de lijst door de tiles te checken in je room met een occupied unit die bij de faction enemy hoort.
    // je hebt alleen nog maar de tile gevonden nu kan je de enemy nog aanspreken.

    //23/3
    //de enemy loopt nu, alleen loopt hij naar random tiles. je wil dat hij richting de player loopt in stappen die niet groter zijn dan de walkrange
    //de enemy attack was je ook al aan begonnen.
}
