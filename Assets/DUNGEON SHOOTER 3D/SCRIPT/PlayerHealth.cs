using System.Collections;
using GamePush;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Vector3 originalLocalPosition; // Dastlabki local pozitsiyani saqlash uchun

    public Transform cameraTransform; 
    public GameObject[] gameObjects; 
    public TextMeshProUGUI text; // Health matni uchun TextMeshPro komponenti
    [SerializeField] private FloatVariable HealthPlayer; 
    [SerializeField] private BoolVariable Main_menu;// ScriptableObject uchun FloatVariable
    public GameObject diedmenu;
    public GameObject Weaponoff;
    public Image image; // UI Image komponenti
    public float fadeDuration = 1.0f; // Fade davomiyligi (soniyalarda)
    [SerializeField] private IntVariable gun_ammo_add;
    [SerializeField] private IntVariable shotgun_ammo_add;
    [SerializeField] private IntVariable rifle_ammo_add;
    [SerializeField] private IntVariable plasma_ammo_add;
    [SerializeField] private IntVariable rocket_launcher_ammo_add;
    [SerializeField] private BoolVariable ContinueButton;
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    [SerializeField] private ScriptableEventNoParam LoadEvent;
    public GameObject weaponui;
    private string unlimitedSymbol = "∞"; // Cheksizlik belgisini qo‘shish
    private void Start()
    {
        LoadData();
        GetComponent<Animator>().enabled = false;
        UpdateHealthText(); // Health matnini yangilash
        originalLocalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        float largeNumber = 1e+30f; // Katta qiymatni belgilash
        
        if (HealthPlayer.Value == largeNumber)
        {
            text.text = unlimitedSymbol; // Cheksizlik belgisi chiqariladi
        }
        else
        {
            UpdateHealthText(); // Agar HealthPlayer qiymati o'zgarsa, matnni yangilash
        }
    }

    private void OnEnable()
    {
        HealtPacks.PlayerHealth += PlayerHealt_UI;
        SaveEvent.OnRaised += SaveData;
    }

    private void OnDisable()
    {
        HealtPacks.PlayerHealth -= PlayerHealt_UI;
        SaveEvent.OnRaised -= SaveData;
    }

    private void PlayerHealt_UI()
    {
        UpdateHealthText(); // UI ni yangilash metodi chaqiriladi
    }

    public void TakeDamage(float amount)
    {
        image.enabled = true;
        StartCoroutine(FadeOut()); // Fade out effektini boshlash
        HealthPlayer.Value = Mathf.Max(HealthPlayer.Value - amount, 0);
        UpdateHealthText(); // Health matnini yangilash
        
        if (HealthPlayer.Value <= 0f)
        {
            Die();
           // Invoke("MouseOn",2f);// O'lish funktsiyasini chaqirish
        }
    }

    void Die()
    {
        
        image.enabled = false;
        GetComponent<Animator>().enabled = true;
        Debug.Log("Player Died");
        Main_menu.Value = true;
        Weaponoff.SetActive(false);
        StartCoroutine(mouseoff());
      
        // Playerning o'lish logikasini shu yerda amalga oshiring
    }

    IEnumerator mouseoff()
    {
        
     
        yield return new WaitForSeconds(2.5f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
       
        
    }
    
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - elapsedTime / fadeDuration);
            image.color = color;
            yield return null;
        }
    }

    // HealthPlayer qiymatini yangilab ko‘rsatadigan metod
    private void UpdateHealthText()
    {
        string healthText = Mathf.Floor(HealthPlayer.Value).ToString();
        text.text = healthText.Length > 3 ? healthText.Substring(0, 3) : healthText;
    }

    public void Continue()
    {
        Weaponoff.SetActive(true);
        Main_menu.Value = false;
        HealthPlayer.Value = 100f;
        ContinueButton.Value = false;
        // Sichqoncha kursorini yashirish va uni o'rnatish
         // Sichqoncha kursorini yashirish
        // Invoke("MouseOff",0.2f);
        GetComponent<Animator>().enabled = false;
        PlayerMovment PM = FindObjectOfType<PlayerMovment>();
        PM.enabled = true;
        BACKMENU BC = FindObjectOfType<BACKMENU>();
        BC.enabled = true;
        MouseLook ML = FindObjectOfType<MouseLook>();
        ML.enabled = true;
        zoom ZM = FindObjectOfType<zoom>();
        ZM.enabled = true;
        weaponui.SetActive(true);
        diedmenu.SetActive(false);
        ResetCameraPosition();
        gun_ammo_add.Value = 80;
        shotgun_ammo_add.Value=50;
        rifle_ammo_add.Value=100;
        plasma_ammo_add.Value=15;
        rocket_launcher_ammo_add.Value=15;
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(true); // Obyektlarni yoqish
        }
        Cursor.lockState = CursorLockMode.Locked;  // Sichqoncha kursorini markazga qotiradi va uni ekrandan yashiradi
        Cursor.visible = false; 
    
       
    }

    
   
    public void ResetCameraPosition()
    {
        // Animatsiya tugagandan keyin kamerani dastlabki pozitsiyasiga qaytarish
        cameraTransform.localPosition = originalLocalPosition;
    }
    
   
   

    public void SaveData()
    {
        PlayerData data = SaveManager.instance.LoadPlayerData();
        data.playerHealth = HealthPlayer.Value;
        SaveManager.instance.SavePlayerData(data);

      
       
    }


    public void LoadData()
    {
        PlayerData data = SaveManager.instance.LoadPlayerData();
        HealthPlayer.Value = data.playerHealth;

       
        
    }
    
    
}
