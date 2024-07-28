using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_trace : MonoBehaviour, IDamageable
{
    public GameObject decalPrefab; // Decal Projector prefabini qo'ying
    public float decalLifetime = 50f; // Decalning yaroqlilik muddati
    public Vector3 rotationAngles; // Aylantirish uchun burchaklar

    // Start is called before the first frame update
    void Start()
    {
        // Bu yerda boshlanishida hech narsa qilish kerak emas
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitNormal)
    {
        // Decalni raycast nuqtasiga joylashtirish
        GameObject decalInstance = Instantiate(decalPrefab, hitPoint, Quaternion.LookRotation(hitNormal));

        // Rotation qo'shish
        decalInstance.transform.Rotate(rotationAngles);

        // Decalni belgilangan vaqtdan keyin o'chirish
        Destroy(decalInstance, decalLifetime);
    }
}