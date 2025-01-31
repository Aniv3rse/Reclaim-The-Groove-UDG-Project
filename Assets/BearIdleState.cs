using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BearIdleState : StateMachineBehaviour
{
    // para saber cuanto tiempo lleva en el estado en caso de necesitar cambiar
    float timer;

    // esto es un tiempo seteado que queremos el oso este en idle siempre y que al acabar pase a caminar por ejemplo
    // para que no sea estatico
    // tiene un valor por default en caso de ocuparlo, pero tambien se puede cambiar en el inspector
    public float idleTime = 4f;


    // transformada del jugador para que el oso sepa donde se encuentra el jugador
    Transform player;


    // flotante para detectar si el jugador esta en rango de chase state
    public float detectionAreaRadius = 18f;



    // SOLO VAMOS A UTILIZAR LAS PRIMERAS 3

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // Este primer estado va a correr cuando el oso primero entra a Idle state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reseteamos el tiempo a 0 cada vez que se entra a idle
        timer = 0;

        // tenemos una referencia al jugador, debido a como estos scripts funcionan de manera que solo uno a la vez existe
        // se puede llamar al jugador pues es el unico personaje al que van a atacar
        // se le asigna al jugador en este contexto el objecto con el tag Player, que es el jugador para usarlo aqui
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks

    // Este estado va a correr cada segundo que el oso este en el estado de Idle
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       // vamos a poner las condiciones a seguir para que la animacion siga en loop, pues como lo recordaremos
       // todas las animaciones las tenemos en loop, es decir que no debemos de decirle al programa que las corra otra vez
       // se corren solitas de manera infinita siempre y cuando se mantenga en el estado que le corresponde.


        // transicion a estado walk
        // esta transicion es sencilla, nomas cuando llega al tiempo final se mueve a walk para que no sea estatico
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isWalking", true);
        }


        // transicion a estado chase
        // esta si es mas complicada, pues se revisa si el vector con la posicion del jugado y la posicion del oso
        // nos da un valor que esta en el rango de deteccion del oso para ir a hacer chase al player
        float distanceFromPlayer = Vector3.Distance( player.position, animator.transform.position );
        if(distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }


    // POR EL MOMENTO NO VAMOS A USAR EL OnStateExit PERO ES SOLO EN ESTE SCRIPT DE IDLE


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state

    // Este estado va a correr el momento que salga el oso del estado Idle y pasa a un diferente state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //   
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
