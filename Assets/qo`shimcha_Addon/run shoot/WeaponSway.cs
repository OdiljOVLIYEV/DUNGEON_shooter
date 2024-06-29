using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount, maxSwaAmount,smoothAmount;
    private Quaternion initialRot;

    void Start()
    {
        initialRot = transform.localRotation;
    }

    void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * swayAmount;
            float mouseY = Input.GetAxis("Mouse Y") * swayAmount;
            mouseX = Mathf.Clamp(mouseX, -maxSwaAmount, maxSwaAmount);
            mouseY = Mathf.Clamp(mouseY, -maxSwaAmount, maxSwaAmount);
            
            Quaternion targetrotX = Quaternion.AngleAxis(-mouseX, Vector3.up);
            Quaternion targetrotY = Quaternion.AngleAxis(mouseY, Vector3.right);
            Quaternion targetRot = initialRot * targetrotX * targetrotY;
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * smoothAmount);
        }
    }