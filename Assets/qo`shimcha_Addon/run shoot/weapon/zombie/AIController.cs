using System.Collections;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public bool canPatrol = true;
    public bool canChase = true;
    public bool canWander = true;
    public bool canHearSound = true;
    public bool canAttack = true;
    public bool canShoot = true; // New shooting capability variable
    public bool stopAgent;

    private Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f; // Distance to inflict damage on player
    public float shootDistance = 8f;
    public float hearSoundRadius = 20f;// Distance to shoot at player
    public float lostDistance = 15f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackDamage = 10f; // Amount of damage
    public float attackCooldown = 1f; // Cooldown between attacks
    private float lastAttackTime;

    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint; // Point from where the projectile is fired
    public float BulletSpeed = 2f;
    public float shootCooldown = 2f; // Cooldown between shots
    private float lastShootTime;

    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    private NavMeshAgent navMeshAgent;
    public Animator anim;
    
    private Vector3 lastHeardSoundPosition = Vector3.zero;
  
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
       // anim = GetComponent<Animator>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopAgent = false;
        lastAttackTime = -attackCooldown; // Initialize cooldown
        lastShootTime = -shootCooldown; // Initialize shoot cooldown
    }

    void Update()
    {
        
        if (stopAgent)
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
                if (player != null)
                {
                    // Player tomonga silliq aylantirish
                    Vector3 direction = (player.position - transform.position).normalized;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    navMeshAgent.transform.rotation = Quaternion.Slerp(navMeshAgent.transform.rotation, targetRotation, Time.deltaTime * 5f);
                }
                ChasePlayer();
                //StartCoroutine(ChaseWithAnimation());
               
                if (canShoot && distanceToPlayer < shootDistance)
                {
                 
                    ShootPlayer();
                }
                else if (canAttack && distanceToPlayer < attackDistance)
                {
                    AttackPlayer();
                }
              
            }
            else if (canPatrol && (distanceToPlayer > lostDistance || !canChase))
            {
                Patrol();
            }
            else if (canWander)
            {
                Wander();
            }
           
            if (canHearSound && lastHeardSoundPosition != Vector3.zero)
            {
                navMeshAgent.SetDestination(lastHeardSoundPosition);
                lastHeardSoundPosition = Vector3.zero;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    
   /* IEnumerator ChaseWithAnimation()
    {
        anim.SetBool("Baqirish", true); // Play "Baqirish" animation
        yield return new WaitForSeconds(1.5f); // Adjust this delay according to your animation length
        anim.SetBool("Baqirish", false); // Stop "Baqirish" animation
        ChasePlayer();
    }*/

    void ChasePlayer()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.SetDestination(player.position);
            anim.SetBool("Walk", true); // Start walking animation
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
            }
            nextPatrolPointIndex = (currentPatrolPoint + 1) % patrolPoints.Length;
            nextDestination = patrolPoints[nextPatrolPointIndex].position;
        }

        navMeshAgent.SetDestination(nextDestination);
        navMeshAgent.speed = patrolSpeed;
        anim.SetBool("Walk", true); // Start walking animation
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
                anim.SetBool("Walk", true); // Start walking animation
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            anim.SetBool("Attack",true);
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
               
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player attacked!");
                StartCoroutine(ResetAttackAnimation());
            }
        }
    }
    IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f); // 0.5 soniya kutish (kerakli vaqtni moslang)
        anim.SetBool("Attack", false);
    }
    void ShootPlayer()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            lastShootTime = Time.time;
            
            StartCoroutine(ShootSequence());
        }
    }

    IEnumerator ShootSequence()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = true;
        }
        anim.SetBool("Shoot", true); // Start shooting animation
        chaseSpeed = 0f;

        // Calculate direction towards the player
        Vector3 direction = (player.position - firePoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(firePoint.position, player.position);

        // Check for obstacles between the enemy and the player
        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, distanceToPlayer))
        {
            // If there is an obstacle and it's not the player, abort shooting
            if (hit.transform != player)
            {
                chaseSpeed = 4f;
                anim.SetBool("Shoot", false); // Stop shooting animation

                if (navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.isStopped = false;
                }

                yield break; // Exit the coroutine early
            }
        }

        // Instantiate projectile if no obstacles are detected
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component and set its velocity
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * BulletSpeed; // Projectile speed
        }

        yield return new WaitForSeconds(0.5f); // Adjust this delay according to your shooting animation length

        chaseSpeed = 4f;
        anim.SetBool("Shoot", false); // Stop shooting animation

        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = false;
        }
    }

   
    public void HeardSound(Vector3 soundPosition)
    {
        if (canHearSound && Vector3.Distance(transform.position, soundPosition) <= hearSoundRadius && navMeshAgent.isOnNavMesh)
        {
            lastHeardSoundPosition = soundPosition;
            navMeshAgent.SetDestination(lastHeardSoundPosition);
            anim.SetBool("Walk", true);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearSoundRadius); // Ovoz tanib olish radiusini chizish
    }
}
