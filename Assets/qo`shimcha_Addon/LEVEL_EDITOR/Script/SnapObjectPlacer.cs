using UnityEngine;
using UnityEngine.UI;

public class SnapObjectPlacer : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject wallPrefab;

    private GameObject selectedPrefab;

    public Button groundButton;
    public Button wallButton;

    private float snapTolerance = 0.5f; // Adjust this value for snapping distance

    void OnEnable()
    {
        // Listen for button clicks
        groundButton.onClick.AddListener(SelectGround);
        wallButton.onClick.AddListener(SelectWall);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }

    public void SelectGround()
    {
        selectedPrefab = groundPrefab;
    }

    public void SelectWall()
    {
        selectedPrefab = wallPrefab;
    }

    void PlaceObject()
    {
        if (selectedPrefab == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = hit.point;

            if (selectedPrefab == wallPrefab)
            {
                Vector3? snappedPosition = SnapToGround(position);

                if (snappedPosition.HasValue)
                {
                    Instantiate(selectedPrefab, snappedPosition.Value, Quaternion.identity);
                }
            }
            else
            {
                Instantiate(selectedPrefab, position, Quaternion.identity);
            }
        }
    }

    Vector3? SnapToGround(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, snapTolerance);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                Vector3 groundPosition = collider.transform.position;
                Vector3 groundScale = collider.transform.localScale;

                // Snap to nearest edge of the ground
                float xDist = Mathf.Abs(position.x - groundPosition.x);
                float zDist = Mathf.Abs(position.z - groundPosition.z);

                if (xDist > zDist)
                {
                    position.x = groundPosition.x + Mathf.Sign(position.x - groundPosition.x) * groundScale.x / 2;
                }
                else
                {
                    position.z = groundPosition.z + Mathf.Sign(position.z - groundPosition.z) * groundScale.z / 2;
                }

                position.y = groundPosition.y + groundScale.y / 2;
                return position;
            }
        }

        return null;
    }
}