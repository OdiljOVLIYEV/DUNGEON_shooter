using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public bool canPatrol = true;
    public bool canChase = true;
    public bool canWander = true;
    [SerializeField] private BoolVariable stopAgent;

    private Transform player;
    public float chaseDistance = 10f;
    public float lostDistance = 15f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopAgent.Value = false;
    }

    void Update()
    {
        if (stopAgent.Value != null && stopAgent.Value)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (canChase && distanceToPlayer < chaseDistance)
        {
            ChasePlayer();
        }
        else if (canPatrol && (distanceToPlayer > lostDistance || !canChase))
        {
            Patrol();
        }
        else if (canWander)
        {
            Wander();
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }

        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
    }

    void Wander()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
            Vector3 finalPosition = hit.position;

            navMeshAgent.speed = patrolSpeed;
            navMeshAgent.SetDestination(finalPosition);
        }
    }
}
