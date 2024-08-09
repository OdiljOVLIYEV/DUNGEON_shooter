
using UnityEngine;

public enum ParticleType
{
    Blood,
    Smoke,
    ExplosiveEffect,
    BulletHole,
    PlasmaEffect
}


public interface IDamageable
{
      void TakeDamage(float amount,Vector3 hitPoint,Vector3 hitNormal);
      
     
}
