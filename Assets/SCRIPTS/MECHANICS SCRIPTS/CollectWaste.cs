using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWaste : MonoBehaviour
{
    //public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        //collectSound.Play();

        ScoringSystem.theScore += 1;

        Destroy(gameObject);
    }
}
