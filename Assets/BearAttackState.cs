using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    // la distancia en la que se detiene de atacar debe de ser mayor a la distancia de ataque
    public float stopAttackingDistance = 2.6f;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Inicializar como todos los demas estados -- //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // vamos a crear un metodo para que cuando la animacion de ataque ocurra
        // el oso se mantiene volteando a ver hacia la direccion del jugador, para que no de el
        // ataque en un area donde no este el jugador.
        LookAtPlayer();

        // -- checa si el agente necesita parar de atacar al jugador -- //
        // si el jugador esta fuera del rango de ataque, lo deja de atacar pero puede que siga en chase
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }

    }


    // esto es para que voltee a ver a la direccion del jugador al atacar
    private void LookAtPlayer()
    {
        // se rota al oso hacia donde esta el jugador
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        // pero se rota manteniendo el eje de las y en 0, eso para que si el jugador esta en una posicion elevada
        // el oso no pueda rotar hacia arriba y mantenga el modelo 3d en contacto con el suelo
        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
