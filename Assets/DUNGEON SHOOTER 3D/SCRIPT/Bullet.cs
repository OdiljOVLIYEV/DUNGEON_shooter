using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage = 10f; // O'qning zarar miqdori
    public float explosionForce = 700f;
    public float explosionRadius = 5f;
    public GameObject explosionEffect;
    public ParticleType particleType;
    public int maxDamage = 100;

    public bool rc_l_for_radius;
   // public GameObject EFFECT;
    private void OnTriggerEnter(Collider other)
    {

        if (rc_l_for_radius)
        {
            Explode();
            
            
        }
        else
        {
            
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {    
                damageable.TakeDamage(damage, transform.position, -transform.forward);
                particalsystem();
                Destroy(gameObject,3f); // O'qni yo'q qilish
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

            // Dushmanlarga zarar etkazish
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float damagePercentage = Mathf.Clamp01((explosionRadius - distance) / explosionRadius);
                float damage = maxDamage * damagePercentage;
                Vector3 hitPoint =
                    nearbyObject.ClosestPoint(transform.position); // Hit point - portlash markaziga yaqin nuqta
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
                // O'q izini devorga joylashtirish
                Instantiate(explosionEffect, transform.position, transform.rotation);// Izni devor ob'ektiga biriktirish
                break;
		
        }
        
    }
}