using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	public float rotationSpeed = 25f;
	public float minAngle = -80f;
	public float maxAngle = 80f;

	private float currentAngle = 0f;

	void Update()
	{
		// Rotate the camera left when pressing the Q key
		if (Input.GetKey(KeyCode.Q))
		{
			RotateCamera(1);
		}

		// Rotate the camera right when pressing the E key
		if (Input.GetKey(KeyCode.E))
		{
			RotateCamera(-1);
		}
	}

	void RotateCamera(float direction)
	{
		float angleToRotate = direction * rotationSpeed * Time.deltaTime;
		currentAngle += angleToRotate;

		// Limit the rotation angle between minAngle and maxAngle
		currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

		transform.localRotation = Quaternion.Euler(0f,0f,currentAngle);
		
	}
}
