using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        // Get the target position
        Vector3 targetPosition = GetTargetPosition();

        // Calculate the direction towards the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Set the bullet's velocity to move towards the target
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    private Vector3 GetTargetPosition()
    {
        // Replace this with your target position logic
        return new Vector3(10f, 0f, 0f);
    }
}