using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    
    /*  esta parte es para la sensibilidad que tiene el mouse, 
        100 es muy poco pero es publica, con lo que se puede cambiar en otras partes
        la idea es que puedas modificar la sensibilidad
    */
    public float mouseSensitivity = 100f;
    
    /*  Esta parte es rotacion en eje x, y
        el eje de las x es para ver de arriba a abajo, etc.
        el eje de las y es para ver de izquierda a derecha, etc.
    */
    float xRotation = 0f;
    float YRotation = 0f;
 
    void Start()
    {
      //    El cursor en el juego se lockea en el centro de la pantalla y es invisible
      Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
       /*   esto son los inputs que guardan los valores de mouse eje x, y, para poder mover los ejes de rotacion
            tambien se aplica lo sensible que es el movimiento del mouse para hacerlo mas rapido o lento, lo multipla for 100 ahorita
            deltaTime es el tiempo en segundos desde el ultimo frame que se pudo leer, esto es porque si juegas en
            30 fps, un input en un segundo es igual a 30, y en 60 fps es igual a 60, por lo que deltaTime nos ayuda a crear consistencia
            entre los dos modos de fps
       */
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       /*   controla rotacion alrededor de eje x (Arriba y abajo) notarlo que se usa mouseY en lugar de X es 
            por como funciona los angulos en unity, y se hace en minus para que no sea invertido, si lo pones
            += mouseY seria rotacion inversa.
       */
       xRotation -= mouseY;
 
       /*   Se controla la rotacion un poquito con clamp para que la cabeza no rote muchisimo, pues tenemos cuello
            es decir clamp es el minimo en float y el maximo en float que permite el juego para rotar
       */ 
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
       //controla rotacion alrededor de eje y (Arriba y abajo)
       YRotation += mouseX;
 
       /*   Aplicar ambas rotaciones, la transform es una de las propiedades del objeto del jugador
            Quaternion es para rotaciones
            Euler es para pasarle las rotaciones en grados
       */
       transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
 
    }
}