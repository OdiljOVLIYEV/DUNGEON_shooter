using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float DestroyTime;

    private void Start()
    {
        Destroy(gameObject,DestroyTime);
    }
}
