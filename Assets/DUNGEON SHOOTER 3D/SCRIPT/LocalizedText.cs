using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string localizationKey;
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        string localizedValue = LocalizationManager.Instance.GetLocalizedValue(localizationKey);
        if (localizedValue == "Localized text not found")
        {
            Debug.LogWarning("Localized text not found for key: " + localizationKey);
        }
        textComponent.text = localizedValue;
    }
}
