
using UnityEngine;

public enum ParticleType
{
    Blood,
    Smoke,
    BulletHole
}


public interface IDamageable
{
     void TakeDamage(float amount,Vector3 hitPoint,Vector3 hitNormal);
}
