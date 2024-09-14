using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    public Button englishButton;
    public Button russianButton;
    public Button turkeyButton;

    void Start()
    {
        // Tugmalarga metodlarni bog'lash
        englishButton.onClick.AddListener(SetLanguageToEnglish);
        russianButton.onClick.AddListener(SetLanguageToRussian);
        turkeyButton.onClick.AddListener(SetLanguageToTurkey);
    }

    public void SetLanguageToEnglish()
    {
        LocalizationManager.Instance.LoadLocalization("en");
        // Matnlarni yangilash
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
    }

    public void SetLanguageToRussian()
    {
        LocalizationManager.Instance.LoadLocalization("ru");
        // Matnlarni yangilash
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
    }
    
    public void SetLanguageToTurkey()
    {
        LocalizationManager.Instance.LoadLocalization("tr");
        // Matnlarni yangilash
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
    }
}