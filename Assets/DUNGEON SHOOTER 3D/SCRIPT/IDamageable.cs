
using UnityEngine;

public enum ParticleType
{
    Blood,
    Smoke,
    ExplosiveEffect,
    BulletHole
    
}


public interface IDamageable
{
     void TakeDamage(float amount,Vector3 hitPoint,Vector3 hitNormal);
}
