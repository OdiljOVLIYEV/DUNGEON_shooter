using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{   public Vector3 rotation;
	public float rotationSpeed = 10f;

	void Update()
    {
    	transform.Rotate(rotation*rotationSpeed*Time.deltaTime);
	}
}
