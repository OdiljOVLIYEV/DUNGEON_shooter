using System;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float damage = 100f; // O'qning zarar miqdori
    public float explosionForce = 700f;
    public float explosionRadius = 5f;
    public GameObject explosionEffect;
    public ParticleType particleType;
    public int maxDamage = 100;

    public bool rc_l_for_radius;
   

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rc_l_for_radius)
        {
            Explode();
            Destroy(gameObject,0.4f); // O'qni yo'q qilish
        }
        else
        {
           
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, transform.position, -transform.forward);
              
                particalsystem();
                Destroy(gameObject,1f);
            }
           
            Destroy(gameObject,1f); 
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

            // Dushmanlarga zarar etkazish
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float damagePercentage = Mathf.Clamp01((explosionRadius - distance) / explosionRadius);
                float damage = maxDamage * damagePercentage;
                Vector3 hitPoint = nearbyObject.ClosestPoint(transform.position); // Hit point - portlash markaziga yaqin nuqta
                Vector3 hitNormal = (nearbyObject.transform.position - transform.position).normalized; // Hit normal

                damageable.TakeDamage(damage, hitPoint, hitNormal);
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
        // Draw the explosion radius as a wireframe sphere in the Scene view
        Gizmos.color = Color.red; // Set the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Draw the wireframe sphere
    }
}
