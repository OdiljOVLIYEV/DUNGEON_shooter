using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items; // Kalit-qiymat juftliklari uchun massiv
}

[System.Serializable]
public class LocalizationItem
{
    public string key;   // Lokalizatsiya kaliti (masalan, "start_button")
    public string value; // Kalitga mos matn (masalan, "Start" yoki "Начать")
}


public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, string> localizedText;
    private string missingTextString = "Localized text not found";
   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalization("tr"); // Boshlanishida rus tilini yuklash
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLocalization(string languageCode)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, $"localization_{languageCode}.json");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            Debug.Log("Localization data: " + dataAsJson); // JSON ma'lumotlarni konsolda tekshirish

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Localization data loaded for: " + languageCode);
        }
        else
        {
            Debug.LogError("Localization file not found: " + filePath);
        }
    }



    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }

        return missingTextString;
    }
}
