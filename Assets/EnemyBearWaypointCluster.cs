using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBearWaypointCluster : MonoBehaviour
{
    /*  
        Es una simple referencia para que sepa de donde de sacan los waypoint clusters, y asi cada objeto
        diferente puede tener un waypoints cluster asociado a si mismo y lo puede seguir a donde este llegue
        le llame enemyBear en especifico porque es un comportamiento para el oso, si quiero generar otros
        animales despues 
    */
    public GameObject enemyBearWaypointCluster;
}
