using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public Slider SLIDER;
    public Slider volumeSlider;
    public float maxvolume;// Ovoz uchun qo'shimcha Slider
    public TextMeshProUGUI text;
    public Transform playerBody;

    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    public Transform targetObject; 
    public float rotationSpeed = 5f;
    public float leftmaxf;
    public float rightmax;

    public float minAngle = -80f;
    public float maxAngle = 80f;

    private float currentAngle = 0f;

    public AudioSource audioSource; // AudioSource komponenti
 
    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100f);
        SLIDER.value = mouseSensitivity / 10f;
        Cursor.lockState = CursorLockMode.Locked;

        // Ovoz darajasini dastlabki qiymatiga o'rnating
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // MusicPlayer skriptini topish
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != null)
        {
            // Slider qiymatiga asoslangan holda musiqa o'yinchi ovozini yangilash
            volumeSlider.onValueChanged.AddListener(musicPlayer.SetMaxVolume);
        }
    }


    // Update is called once per frame
    void Update()
    {  
        // Convert the slider value to a string and truncate it to 3 characters
        string sliderValueText = Mathf.Floor(SLIDER.value).ToString();
        text.text = sliderValueText.Length > 3 ? sliderValueText.Substring(0, 3) : sliderValueText;

        // Update the mouse sensitivity in PlayerPrefs
        PlayerPrefs.SetFloat("currentSensitivity", mouseSensitivity);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Find necessary components
        lefthandcheck chap = FindObjectOfType<lefthandcheck>();
        righthandcheck ong = FindObjectOfType<righthandcheck>();
        PlayerMovment move = FindObjectOfType<PlayerMovment>();

        // Wallrun rotation adjustments
        if (chap.chap == true && move.wallrun == true)
        {
            RotateCamera(-1); 	
        }
        if (ong.ong == true && move.wallrun == true)
        {
            RotateCamera(1);
        }
    }

    public void adjustspeed(float newspeed)
    {
        mouseSensitivity = newspeed * 25;
    }

    void RotateCamera(float direction)
    {
        float angleToRotate = direction * rotationSpeed * Time.deltaTime;
        currentAngle += angleToRotate;

        // Limit the rotation angle between minAngle and maxAngle
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    public void SetVolume(float volume)
    {
        
        audioSource.volume = volume; // Ovoz darajasini o'zgartirish
        maxvolume = audioSource.volume;
        // MusicPlayer ovoz balandligini yangilash
    }

}
