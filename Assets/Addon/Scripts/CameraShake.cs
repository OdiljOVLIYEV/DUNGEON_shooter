using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float bobbingSpeed = 0.18f; // Speed of the bobbing
	public float bobbingAmount = 0.2f; // Amount of the bobbing

	private float defaultPosY; // Default Y position of the camera
	private float timer = 0.0f;

	void Start()
	{
		defaultPosY = transform.localPosition.y;
	}

	void Update()
	{
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
		{
			// If the player is moving, calculate the bobbing motion
			timer += Time.deltaTime * bobbingSpeed;
			float yPos = defaultPosY + Mathf.Sin(timer) * bobbingAmount;
			transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
		}
		else
		{
			// If the player is not moving, reset the position
			timer = 0.0f;
			transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z);
		}
	}
}
