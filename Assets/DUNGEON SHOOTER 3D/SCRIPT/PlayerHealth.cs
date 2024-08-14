using System.Collections;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI text; // Health matni uchun TextMeshPro komponenti
    [SerializeField] private FloatVariable HealthPlayer; // ScriptableObject uchun FloatVariable
    
    public Image image; // UI Image komponenti
    public float fadeDuration = 1.0f; // Fade davomiyligi (soniyalarda)
    
    private string unlimitedSymbol = "∞"; // Cheksizlik belgisini qo‘shish
    private void Start()
    {
        GetComponent<Animator>().enabled = false;
        UpdateHealthText(); // Health matnini yangilash
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
    }

    private void OnDisable()
    {
        HealtPacks.PlayerHealth -= PlayerHealt_UI;
    }

    private void PlayerHealt_UI()
    {
        UpdateHealthText(); // UI ni yangilash metodi chaqiriladi
    }

    public void TakeDamage(float amount)
    {
        image.enabled = true;
        StartCoroutine(FadeOut()); // Fade out effektini boshlash
        HealthPlayer.Value -= amount;
        UpdateHealthText(); // Health matnini yangilash
        
        if (HealthPlayer.Value <= 0f)
        {
            Die(); // O'lish funktsiyasini chaqirish
        }
    }

    void Die()
    {
        image.enabled = false;
        GetComponent<Animator>().enabled = true;
        Debug.Log("Player Died");
        // Playerning o'lish logikasini shu yerda amalga oshiring
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
}
