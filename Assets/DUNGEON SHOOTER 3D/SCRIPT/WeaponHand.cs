using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{
    public float damage = 10f;
    public LayerMask enemyLayer;

    public ParticleSystem Swordeffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        WeaponSwitcher.SwordEffectCall += SwordPartical;
    }

    private void OnDisable()
    {
        WeaponSwitcher.SwordEffectCall -= SwordPartical;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is in the enemy layer
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - hitPoint;
                damageable.TakeDamage(damage, hitPoint, hitNormal);
                Debug.Log(other.gameObject.name);
            }
        }
    }

    public void SwordPartical()
    {
        
        Swordeffect.Play();
        
    }
}
