using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public bool canPatrol = true;
    public bool canChase = true;
    public bool canWander = true;
    public bool canHearSound = true;
    public bool canAttack = true;
    public bool canShoot = true;
    public bool uloqtirsin = false;
    public bool canCharge = true; // New charging capability variable
    public bool stopAgent;

    private Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float shootDistance = 8f;
    public float chargeDistance = 5f; // Distance at which charging starts
    public float hearSoundRadius = 20f;
    public float lostDistance = 15f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chargeSpeed = 6f; // Speed when charging
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float BulletSpeed = 2f;
    public float shootCooldown = 2f;
    private float lastShootTime;

    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    private NavMeshAgent navMeshAgent;
    public Animator anim;

    private Vector3 lastHeardSoundPosition = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopAgent = false;
        lastAttackTime = -attackCooldown;
        lastShootTime = -shootCooldown;
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
                ChasePlayer();

                if (canShoot && distanceToPlayer < shootDistance)
                {
                    ShootPlayer();
                }

                // Ensure that AttackPlayer is called when within attack distance, regardless of shooting
                if (canAttack && distanceToPlayer < attackDistance)
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
                anim.SetBool("Walk", true);
            }
        }

    }


    void ChasePlayer()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            // Determine the speed based on whether the enemy should charge
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            float currentSpeed = canCharge && distanceToPlayer <= chargeDistance ? chargeSpeed : chaseSpeed;

            navMeshAgent.speed = currentSpeed;
            navMeshAgent.SetDestination(player.position);
            anim.SetBool("Walk", true);
            
            if (canCharge == true)
            {
                if (currentSpeed == chargeSpeed)
                {
                    anim.SetBool("Charge", true);
                    
                }
                else
                {
                    anim.SetBool("Charge", false);
                }
                // Optionally trigger the charging animation if within charge range

                // Set the walking animation
              

                // Check if the agent has a valid path
            }
        }
    }

    void PushPlayerAway()
    {
        if (Vector3.Distance(transform.position, player.position) <= chargeDistance)
        {
            // Calculate the direction away from the enemy
            Vector3 pushDirection = (player.position - transform.position).normalized;

            // Ensure you get the CharacterController component from the player
            CharacterController playerController = player.GetComponent<CharacterController>();

            if (playerController != null)
            {
                // Apply force to the player's position to push them away
                playerController.Move(pushDirection * chargeSpeed * Time.deltaTime);
            }
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

    void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Ensure the player is within attack range
        if (distanceToPlayer <= attackDistance)
        {
            // Check if the cooldown period has passed
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                // Update the last attack time
                lastAttackTime = Time.time;
                anim.SetBool("Attack", true);

                // Deal damage to the player
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }

                // Reset the attack animation after a short delay
                StartCoroutine(ResetAttackAnimation());
            }
        }
    }

    IEnumerator ResetAttackAnimation()
    {
        PushPlayerAway();
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attack", false);
        // Hujumdan keyin normal holatga qaytish
        // Yoki harakatni davom ettirish
        if (canChase)
        {
            ChasePlayer();
        }
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
        anim.SetBool("Shoot", true);
        chaseSpeed = 0f;
        if (uloqtirsin == true)
        {
            // Dushman granatasini yaratish va pozitsiyasini belgilash
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

// Granataning RigidBody komponentini olish
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // O'yinchiga nisbatan granata uloqtirish yo'nalishini hisoblash
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Tashlash burchagini sozlash uchun yuqori yo'nalishni qo'shish
                float heightAdjustment = 2f; // Tashlash burchagini oshirish uchun
                Vector3 throwDirection = directionToPlayer + Vector3.up * heightAdjustment;

                // Granatani uloqtirish
                rb.AddForce(throwDirection * BulletSpeed, ForceMode.VelocityChange);
            }
 
        }
        else
        {

            Vector3 direction = (player.position - firePoint.position).normalized;
            float distanceToPlayer = Vector3.Distance(firePoint.position, player.position);

            if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, distanceToPlayer))
            {
                if (hit.transform != player)
                {
                    chaseSpeed = 4f;
                    anim.SetBool("Shoot", false);

                    if (navMeshAgent.isOnNavMesh)
                    {
                        navMeshAgent.isStopped = false;
                    }

                    yield break;
                }
            }

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * BulletSpeed;
            }
        }

        yield return new WaitForSeconds(0.5f);

        chaseSpeed = 4f;
        anim.SetBool("Shoot", false);

        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = false;
        }
    }

    public void HeardSound(Vector3 soundPosition)
    {
        if (canHearSound && Vector3.Distance(transform.position, soundPosition) <= hearSoundRadius)
        {
            lastHeardSoundPosition = soundPosition;
            navMeshAgent.SetDestination(lastHeardSoundPosition);
            anim.SetBool("Walk", true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearSoundRadius);

        if (navMeshAgent != null && navMeshAgent.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, navMeshAgent.destination);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    
   
}
