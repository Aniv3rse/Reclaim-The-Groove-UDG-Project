using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    // chase speed con un incremento en cuanto al walk, pues es normal que un oso al perseguir vaya a correr mas rapido
    public float chaseSpeed = 6f;

    // la distancia en donde el oso va a detenerse, esto es en si otro radio de deteccion pero es para detenerse
    // si el jugador se aleja del flotante establecido, el oso se detiene
    // tambien se requiere que esto sea un poc mas grande que el radio de deteccion, esto para que se detenga 
    // y no vuelva a querer perseguir el jugador
    public float stopChasingDistance = 21;

    // esto es un radio de deteccion pero para que el oso ataque, si el jugador se encuentra en este radio pasa a attackState
    public float attackingDistance = 2.5f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // -- Inicializar como todos los demas estados -- //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        // la velocidad de persecusion es igual a la que pusimos anteriormente en chaseSpeed, para que sea mas rapido que caminando
        agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // le pasamos al oso es decir el agente la posicion del jugador para que lo tenga como destino
        // esto hace que siga al jugador como si lo estubiera persiguiendo a tiempo real
        // adicionalmente el LookAt(player) hace que el modelo del oso voltee a ver al jugador en todo momento de la persecusion
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        // calcula que tan cerca esta del jugador a todo momento
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // -- hace un chequeo si es que se debe de detenerse en cualquier momento de la persecusion -- //
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

        // -- hace un chequeo si es que debe de empezar a atacar en cualquier momento de la persecusion -- //
        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // se debe detener al agente para que no se mueva una vez que salga de este estado
        // todos los estados en donde se mueva van a tener la informacion para que se mueva otra vez
        // en caso de que un NPC pueda atacar y moverse al mismo tiempo esto NO deberia de existir en su codigo
        // pero para el oso si nos sirve, o por lo menos la manera que lo hacemos aqui
        agent.SetDestination(agent.transform.position);
    }

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
