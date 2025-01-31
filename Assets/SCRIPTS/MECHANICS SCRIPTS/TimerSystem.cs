using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para poder tener el timer se utiliza esto
using TMPro;

public class TimeSystem : MonoBehaviour
{
    // se crea una variable le puse el mismo nombre que mi texto para que almacene el texto del tiempo transcurrido
    [SerializeField] TextMeshProUGUI timerText;

    // se hace un flotante del tiempo trascurrido
    float elapsedTime;

    void Update()
    {
        // obtenemos el delta time y lo almacenamos en elapsed time
        elapsedTime += Time.deltaTime;
        // vamos a dividir el tiempo entre minutos y segundos
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // despues lo hacemos string y lo pasamos a timerText recordar formatear el texto a la manera que lo queremos
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
