using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public GameObject[] objectsToPlace; // Joylashtirish uchun objectlar arrayi
    private int currentObjectIndex = -1; // Hozirgi tanlangan object (-1 - tanlanmagan)
    private GameObject lastPlacedObject; // Oxirgi joylashtirilgan object

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentObjectIndex >= 0)
        {
            // Hozirgi tanlangan object prefabini olish
            GameObject objectToPlace = objectsToPlace[currentObjectIndex];

            // Sahnadagi joylashuv pozitsiyasini hisoblash
            Vector3 placementPosition;
            if (lastPlacedObject != null && objectToPlace.CompareTag("Wall"))
            {
                Bounds lastBounds = lastPlacedObject.GetComponent<Renderer>().bounds;
                Bounds currentBounds = objectToPlace.GetComponent<Renderer>().bounds;

                // Yangi objectni oxirgi joylashtirilgan devor objectiga yopishtirish
                placementPosition = lastBounds.max - new Vector3(currentBounds.extents.x, lastBounds.extents.y - currentBounds.extents.y, 0);
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; // Kamera oldidagi masofa (camera'dan objectgacha bo'lgan masofa)
                placementPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            }

            // Yangi objectni joylashtirish
            lastPlacedObject = Instantiate(objectToPlace, placementPosition, Quaternion.identity);
        }
    }

    public void SetCurrentObjectIndex(int index)
    {
        currentObjectIndex = index;
    }
}