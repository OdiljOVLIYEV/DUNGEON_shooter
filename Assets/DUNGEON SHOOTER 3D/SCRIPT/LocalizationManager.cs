using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

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
    public System.Action OnLocalizationLoaded; // Lokalizatsiya yuklanganda chaqiriladigan callback

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalization("ru"); // Boshlanishida rus tilini yuklash
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

    #if UNITY_WEBGL && !UNITY_EDITOR
       filePath = "StreamingAssets/localization_" + languageCode + ".json";
    #endif


        StartCoroutine(LoadLocalizationFile(filePath));
    }

    private IEnumerator LoadLocalizationFile(string filePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Localization file not found: " + filePath);
        }
        else
        {
            string dataAsJson = request.downloadHandler.text;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                string key = loadedData.items[i].key;
                string value = loadedData.items[i].value;

                if (!localizedText.ContainsKey(key))
                {
                    localizedText.Add(key, value);
                }
                else
                {
                    Debug.LogWarning($"Duplicate key found: {key}. Skipping this entry.");
                }
            }

            Debug.Log("Localization data loaded successfully.");

            // Lokalizatsiya yuklangandan keyin callbackni chaqiramiz
            OnLocalizationLoaded?.Invoke();
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

