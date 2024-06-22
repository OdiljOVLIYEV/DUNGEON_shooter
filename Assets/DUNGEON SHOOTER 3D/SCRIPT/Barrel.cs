using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    
    public int health = 100;
    public GameObject explosionEffect;
    public ParticleType particleType;
    public float explosionForce = 700f;
    public float explosionRadius = 5f;
    public int maxDamage = 100;
    public GameObject[] debrisPrefabs; 
    void Update()
    {
        if (health <= 0)
        {
            Explode();
        }
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= Mathf.RoundToInt(amount);
    }

    void Explode()
    {
        // Portlash effektini yaratish
        switch (particleType)
        {
			
            case ParticleType.ExplosiveEffect:
                // O'q izini devorga joylashtirish
                Instantiate(explosionEffect, transform.position, transform.rotation);// Izni devor ob'ektiga biriktirish
                break;
		
        }
       
        foreach (GameObject debrisPrefab in debrisPrefabs)
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Random.rotation);
            Rigidbody rb = debris.GetComponent<Rigidbody>();
           
            if (rb != null)
            {
                Vector3 explosionDir = (debris.transform.position - transform.position).normalized;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                MeshCollider collider = debris.GetComponent<MeshCollider>();
                collider.convex = true;
            }
        }
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

        // Bochka obyektini yo'q qilish
        Destroy(gameObject);
    }
}