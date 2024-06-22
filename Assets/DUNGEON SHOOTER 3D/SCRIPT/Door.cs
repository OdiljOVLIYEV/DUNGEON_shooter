using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorTransform;
    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float openSpeed = 2f;
    private bool isOpening = false;
    private bool isClosing = false;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    void Start()
    {
        // Boshlanish holatida eshik yopilgan bo'lishi kerak
        AudioSource = GetComponent<AudioSource>();
        doorTransform.localPosition = closedPosition;
    }

    void Update()
    {
        if (isOpening)
        {
            doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, openPosition, Time.deltaTime * openSpeed);
            if (Vector3.Distance(doorTransform.localPosition, openPosition) < 0.01f)
            {
                isOpening = false;
            }
        }

        if (isClosing)
        {
            doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, closedPosition, Time.deltaTime * openSpeed);
            if (Vector3.Distance(doorTransform.localPosition, closedPosition) < 0.01f)
            {
                isClosing = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Eshik ochiq");
           
            isOpening = true;
            isClosing = false;
            AudioSource.PlayOneShot(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DoorCloseTime());

        }
    }

    IEnumerator DoorCloseTime()
    {

       yield return new WaitForSeconds(2f);
       isClosing = true;
       isOpening = false;
       AudioSource.PlayOneShot(closeSound);

    }
}
