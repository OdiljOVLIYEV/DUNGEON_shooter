using System;
using System.Collections.Generic;
using System.IO;
using Obvious.Soap;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float playerHealth=100f;
    public int Ammogun = 80;
    public int AmmoShotgun = 50;
    public int AmmoSuperShotgun = 50;
    public int Machinegun = 100;
    public int Plasma = 15;
    public int RocketLauncher = 15;
    public int currentWave;
    public float EnemyMax;
    public int moneycount = 100000; 
    public List<bool> weaponUnlockStates = new List<bool>(); 
    public List<string> purchasedWeaponIDs = new List<string>();
    
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private string filePath;
    [SerializeField] private ScriptableEventNoParam Reset;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(PlayerData data)
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(data, true));
        Debug.Log("Player data saved.");
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found. Using default values.");
            return new PlayerData(); // Default values
        }
    }

    private void OnEnable()
    {
        Reset.OnRaised += ResetPlayerData;
    }

    private void OnDisable()
    {
        Reset.OnRaised -= ResetPlayerData;
    }

    public void ResetPlayerData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath); // Faylni o'chirib tashlaymiz
            Debug.Log("Player data reset. Save file deleted.");
        }
        else
        {
            Debug.LogWarning("Save file not found, nothing to reset.");
        }
    }
}