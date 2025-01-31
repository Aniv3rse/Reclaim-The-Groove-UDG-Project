using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearWalkState : StateMachineBehaviour
{
    // el oso camina por un tiempo determinado
    float timer;
    public float walkingTime = 10f;

    // referencia al jugado y el agente de NavMeshAgent se necesita para que la inteligencia artificial funcione 
    Transform player;
    NavMeshAgent agent;

    // el radio de deteccion para cambiar de walk a chase es igual al de walk
    // tambien se tiene la velocidad del oso
    public float detectionAreaRadius = 18f;
    public float walkSpeed = 2f;

    // lista de transformadas para que el oso sepa donde debe ir con NavMeshAgent, estas estaran alrededor del oso
    List<Transform> waypointList = new List<Transform>();



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- inicializamos los datos que ocupa el oso -- //
        // como no podemos jalar la referencia del jugador debemos de hacer la busque
        // para referencia del agente se necesita el animator que ya viene echo desde antes en unity es como para ayudar con la tarea
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        // se le cuenta cual es la velocidad, ya la seteamos antes y el temporalizador se pone a 0
        agent.speed = walkSpeed;
        timer = 0;

        // -- para que obtenga todos los waypoints y se pueda mover al primer waypoint -- //
        /* 
            El waypointCluster va a tener los waypoints que vamos a crear, nuevamente se le dice que los encuentre para referencia
            el ciclo es para que pueda adicionar todos los waypoints a la lista uno por uno.
            se utiliza animator.GetComponent en lugar de FindGameObjectWithTag por que queremos hacer mas facil
            que cada enemigo tenga su propio cluster.
        */
        GameObject waypointCluster = animator.GetComponent<EnemyBearWaypointCluster>().enemyBearWaypointCluster; // GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointList.Add(t);
        }

        // creamos un vector random para que se mueva utilizando los waypoints como el rango en el que puede estar
        // el set destination va a decirle a que lugar se debe de mover
        Vector3 firstPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        agent.SetDestination(firstPosition);
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Si el agente llega al waypoint, moverse al siguiente waypoint -- //
        // en si despues de llegar al primer waypoint luego se le asigna otro waypoint y se pasa a las siguientes funciones
        // de manera secuancial, y para cuando tenga que moverse otra vez ya sabe a donde llegar.
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].position);
        }

        // -- transicion a idle state -- //
        // si su tiempo pasa y no encuentra al jugador durante su caminata, simpelemente pasa a idle otra vez
        timer += Time.deltaTime;
        if (timer > walkingTime)
        {
            animator.SetBool("isWalking", false);
        }


        // transicion a estado chase es igual a la que esta en el script de idle state
        // esta si es mas complicada, pues se revisa si el vector con la posicion del jugado y la posicion del oso
        // nos da un valor que esta en el rango de deteccion del oso para ir a hacer chase al player
        float distanceFromPlayer = Vector3.Distance( player.position, animator.transform.position );
        if(distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // se debe detener al agente para que no se mueva una vez que salga de este estado
        // todos los estados en donde se mueva van a tener la informacion para que se mueva otra vez
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
