using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.1f, maxSwayAmount = 0.2f, smoothAmount = 0.15f;
    public float movementSwayMultiplier = 0.5f; // Harakat ta'sirini sozlash uchun ko'rsatkich
    private Quaternion initialRot;

    void Start()
    {
        initialRot = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;
        float movementX = Input.GetAxis("Horizontal") * movementSwayMultiplier;
        //float movementY = Input.GetAxis("Vertical") * movementSwayMultiplier;

        // Harakatga asoslangan tebranishni hisoblash
        mouseX += movementX;
      //  mouseY += movementY;

        mouseX = Mathf.Clamp(mouseX, -maxSwayAmount, maxSwayAmount);
        mouseY = Mathf.Clamp(mouseY, -maxSwayAmount, maxSwayAmount);

        Quaternion targetRotX = Quaternion.AngleAxis(-mouseX, Vector3.up);
        Quaternion targetRotY = Quaternion.AngleAxis(mouseY, Vector3.right);
        Quaternion targetRot = initialRot * targetRotX * targetRotY;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * smoothAmount);
    }
}