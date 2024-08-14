using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform player;
    public float attackDamage = 10f;
    public float explosionForce = 700f;
    public float explosionRadius = 5f;
    public GameObject explosionEffect;
    public ParticleType particleType;
    public int maxDamage = 100;

    public bool rc_l_for_radius;
    public float gravityMultiplier = 50f;  // Gravitasiyani kuchaytirish

    private Rigidbody rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        if (rc_l_for_radius)
        {
            if (rb != null)
            {
                // Gravitasiyani kuchaytirish
                rb.useGravity = true;
                rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Agar o'q portlash radiusida bo'lsa
        if (rc_l_for_radius)
        {
            Explode();
            Destroy(gameObject,0.3f);
            // O'qni yo'q qilish
        }
        else
        {
            // Agar o'q playerga to'g'ridan-to'g'ri to'qnashsa
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("Player attacked!");
                    Destroy(gameObject);
                }
            }
        }
    }

    void Explode()
    {
        // Portlash effektini yaratish
        particalsystem();

        // Yaqin atrofdagi obyektlarga ta'sir qilish
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Agar dushman player bo'lsa va zarar etkazish
            if (nearbyObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                    float damagePercentage = Mathf.Clamp01((explosionRadius - distance) / explosionRadius);
                    float damage = maxDamage * damagePercentage;
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Player attacked!");
                    Destroy(gameObject, 1f);
                }
            }
        }
    }

    private void particalsystem()
    {
        switch (particleType)
        {
            case ParticleType.ExplosiveEffect:
                // Portlash effektini yaratish
                Instantiate(explosionEffect, transform.position, transform.rotation);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos yordamida portlash radiusini chizish
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
