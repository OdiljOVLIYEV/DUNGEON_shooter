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
    private Animator anim;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopAgent.Value = false;
    }

    void Update()
    {
        if (stopAgent != null && stopAgent.Value)
        {
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = true;
            }
            return;
        }
        else
        {
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = false;
            }
        }

        if (navMeshAgent.isOnNavMesh)
        {
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
    }

    void ChasePlayer()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.SetDestination(player.position);
            anim.SetBool("Walk", true);
        }
    }

    void Patrol()
    {
        
        if (patrolPoints.Length == 0)
            return;
       
        int nextPatrolPointIndex = (currentPatrolPoint + 1) % patrolPoints.Length;
        Vector3 nextDestination = patrolPoints[nextPatrolPointIndex].position;

        if (navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolPoint = nextPatrolPointIndex;
            if (currentPatrolPoint == 0)
            {
                Debug.Log("Returned to the first patrol point.");
                // Qo'shimcha harakatlar yoki logikani bu erda amalga oshirishingiz mumkin
            }
            nextPatrolPointIndex = (currentPatrolPoint + 1) % patrolPoints.Length;
            nextDestination = patrolPoints[nextPatrolPointIndex].position;
        }

        navMeshAgent.SetDestination(nextDestination);
        navMeshAgent.speed = patrolSpeed;
        anim.SetBool("Walk", true);
    }

    void Wander()
    {
        if (navMeshAgent.isOnNavMesh)
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
                anim.SetBool("Walk", true);
            }
        }
    }

    void AnimationWalk()
    {
        
        anim.SetBool("Walk",true);
        
    }
}