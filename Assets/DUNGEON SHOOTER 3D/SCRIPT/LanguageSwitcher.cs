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
        LocalizationManager.Instance.OnLocalizationLoaded = UpdateAllLocalizedTexts;
        LocalizationManager.Instance.LoadLocalization("en");
    }

    public void SetLanguageToRussian()
    {
        LocalizationManager.Instance.OnLocalizationLoaded = UpdateAllLocalizedTexts;
        LocalizationManager.Instance.LoadLocalization("ru");
    }

    public void SetLanguageToTurkey()
    {
        LocalizationManager.Instance.OnLocalizationLoaded = UpdateAllLocalizedTexts;
        LocalizationManager.Instance.LoadLocalization("tr");
    }

    private void UpdateAllLocalizedTexts()
    {
        // Matnlarni yangilash
        foreach (var localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }
    }
}