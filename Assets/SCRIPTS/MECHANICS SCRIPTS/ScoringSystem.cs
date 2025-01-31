using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adicionamos unity engine UI para que pueda hacer referencia al UI de manera facil
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    void Update()
    {

        scoreText.GetComponent<Text>().text = "RECOLECTADOS: " + theScore;

    }

}
