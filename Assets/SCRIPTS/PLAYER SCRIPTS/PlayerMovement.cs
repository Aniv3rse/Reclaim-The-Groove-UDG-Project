using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
 
    /*  gravity es la gravedad real multiplicada por 2 por que estaba medio lenta
        jump height es cuantas unidades de medida nos elevamos al presionar boton jump
    */
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    /*  esta parte de groundcheck es para revisar que estemos en una superficie y solo deja saltar cuando estamos en una superficie
        el ground distance es como para ver que tan cerca estamos de una superficie
        groundMask es la capa de la superficie

        la groundMask debe de ser indicada por mi, porque puede que haya superficies donde no quiera que se pueda realizar
        por ejemplo existen layers para agua, por eso es importante indiccar cual layer sera para ground

        escaleras y otras superficies son ground layer
    */
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
 
    // velocidad de caida
    Vector3 velocity;
 
    // boleano cuando si estamos en superficie
    bool isGrounded;
 
    // Update es llamado una vez por cada frame
    void Update()
    {
        // checamos si tocamos superficie, para resetear la velocidad de caida, y que no se haga mas rapido para la siguiente vez
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // se resetea la velocidad por que si no tienes esto es como si siguieras callendo aunque la superficie te paro
        // y la velocidad de caida crece
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        //inpus de teclado, horizonal revisa todos los inputs que pueden ser horizontales, ejemplo (a, d)
        // el vertical checa for ejemplo (w, s), los numeros son nomas 1 o -1 de los dos, para identificar cual direccion
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        // el vector move checa el axis de right (eje rojo en la figura) que es el horizontal en unity, asi se llama por default
        // el vecor move tambien checa el axis de forward (eje azur en la figura) que es el de arriba o abajo
        // despues se pasa a controller
        Vector3 move = transform.right * x + transform.forward * z;
 
        // la instruccion de move, le damos la direccion, la velocidad y el tiempo para el tema de los fps, que 60 sea igual a 30
        // es un vector
        controller.Move(move * speed * Time.deltaTime);
 
        // checamos si el jugador esta en una superficie para que pueda saltar,
        // solo tengo salto positivo es decir hacias arriba con barra space
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // la ecuacion del jumping la velocidad de y sera mathf. 
            // raiz cuadrada de altura de salto multiplicada for -2f multiplicado por gravedad
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // es para lo mismo que sea consistente en 30 y 60 fps, que la gravedad sea constante en ambos modos
        velocity.y += gravity * Time.deltaTime;
 
        // para que el movimiento sea consistente tambien en 30 y 60 fps y sea constante en ambos modos
        controller.Move(velocity * Time.deltaTime);
    }
}