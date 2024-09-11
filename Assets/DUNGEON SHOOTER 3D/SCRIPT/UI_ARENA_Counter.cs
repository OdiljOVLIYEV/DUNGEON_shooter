using System;
using System.Collections;
using System.Collections.Generic;
using GamePush;
using Obvious.Soap;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ARENA_Counter : MonoBehaviour
{
    public TextMeshProUGUI EnemyCountText;
    public TextMeshProUGUI MoneyCountText;
    public TextMeshProUGUI WaveCountText;
    public TextMeshProUGUI TimeCountText;
    public Image wavebackgroud;
    [SerializeField] private BoolVariable Main_menu;
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    public GameObject[] gameObjects; 
    public int NextWaveAddEnemyCounter;
    [SerializeField] private IntVariable moneyCount;
    public static Action MusicManagerStart;
    public static Action MusicManagerPause;
    public static Action MusicManagerResume;
    public static Action<int> OnNewWaveStart;
    private int Wave_number;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public float MaxEnemy;
    private int SpawnEnemy;
    public float SpawnTime;
    [SerializeField] private FloatVariable KillEnemy_UI;
    private float timewave;
    public float Roundtime;
    [HideInInspector]
    public int EnemyRank;
    [SerializeField] private FloatVariable HealthPlayer;
    public GameObject WaveWin;
    public bool NextUI;
    private void Start()
    {
        LoadData();
        MusicManagerStart?.Invoke();
        MusicManagerPause?.Invoke();   
        WaveCountText.enabled = false;
        TimeCountText.enabled = true;
        timewave = Roundtime;
        StartCoroutine(TimeWave());
        
    }

    void Update()
    {
        if (Main_menu.Value == true)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.SetActive(false); // Obyektlarni yoqish
            }
        }
        else
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.SetActive(true); // Obyektlarni o'chirish
            }
        }
       
        
        
        if (MoneyCountText != null)
        {
            MoneyCountText.text = moneyCount.ToString() + " $";
        }

        if (timewave == 0)
        {
            UpdateEnemyCount();
        }
    }

    public void ReceiveWaveCommand(Dictionary<int, int> manualEnemyCounts)
    {
        List<int> enemyIndicesToSpawn = new List<int>();
        int totalEnemies = 0;

        // Calculate the total number of manually specified enemies
        foreach (var entry in manualEnemyCounts)
        {
            totalEnemies += entry.Value;
        }

        // If total enemies exceed MaxEnemy, adjust the counts
        if (totalEnemies > MaxEnemy)
        {
            Debug.LogWarning("Total enemies exceed MaxEnemy. Adjusting to fit MaxEnemy.");

            int remainingEnemies = (int)MaxEnemy;
            foreach (var entry in manualEnemyCounts)
            {
                int adjustedCount = Mathf.Min(entry.Value, remainingEnemies);
                remainingEnemies -= adjustedCount;

                // Add the adjusted number of enemies for this index to the spawn list
                for (int i = 0; i < adjustedCount; i++)
                {
                    enemyIndicesToSpawn.Add(entry.Key);
                }

                // Stop adding more enemies if we've reached the MaxEnemy limit
                if (remainingEnemies <= 0)
                {
                    break;
                }
            }
        }
        else
        {
            // Add the exact number of enemies for each index to the spawn list
            foreach (var entry in manualEnemyCounts)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    enemyIndicesToSpawn.Add(entry.Key);
                }
            }
        }

        // Fill the remaining slots with random enemies if needed
        int remainingRandomEnemies = (int)MaxEnemy - enemyIndicesToSpawn.Count;
        for (int i = 0; i < remainingRandomEnemies; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, EnemyRank);
            if (randomIndex >= 0 && randomIndex < enemies.Length)
            {
                enemyIndicesToSpawn.Add(randomIndex);
            }
        }

        // Start spawning enemies based on the final list
        StartCoroutine(Enemydrop(enemyIndicesToSpawn));
    }



    public void spawn(int enemyIndex)
    {
        if (enemies.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("Enemies or spawn points not set!");
            return;
        }

        // Ensure the enemy index is within bounds
        enemyIndex = Mathf.Clamp(enemyIndex, 0, enemies.Length - 1);
        GameObject enemyToSpawn = enemies[enemyIndex];

        // Randomly select a spawn point
        int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Instantiate the enemy at the random spawn point
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }



    private void UpdateEnemyCount()
    {
        if (EnemyCountText != null)
        {
            EnemyCountText.text = KillEnemy_UI.ToString();
        }
        if (KillEnemy_UI == 0&& NextUI==true)
        {
             NextUI = false;
             MusicManagerPause?.Invoke();
           
             SaveData();
             SaveEvent.Raise();
             StartCoroutine(WIN());
            
        }
       
    }
    IEnumerator WIN()
    {
        
     
        yield return new WaitForSeconds(2f);
        Main_menu.Value = true;
        WaveWin.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale=0f;
        
       
        
    }
    
    public void next()
    {  
        ShowFullscreen();
        WaveWin.SetActive(false);
        Debug.Log("keyingi");
        timewave = Roundtime;
        StartCoroutine(TimeWave());
        Cursor.lockState = CursorLockMode.Locked;  // Sichqoncha kursorini markazga qotiradi va uni ekrandan yashiradi
        Cursor.visible = false; 
        Main_menu.Value = false;
    }

   
    private void UpdateWaveCount()
    {
        if (WaveCountText != null)
        {
            WaveCountText.text = "Wave " + Wave_number.ToString();
            
        }
    }

    IEnumerator TimeWave()
    {
       
        
        while (timewave > 0)
        {
            
            
           
            yield return new WaitForSeconds(1f);
            timewave -= 1;
            TimeCountText.text = timewave.ToString();
            WaveCountText.enabled = false;
            TimeCountText.enabled = true;

            if (timewave == 0)
            {
                NextUI = true;
                WaveCountText.enabled = true;
                TimeCountText.enabled = false;
                
                MaxEnemy += NextWaveAddEnemyCounter;
                SpawnEnemy = (int)MaxEnemy;
                KillEnemy_UI.Value = MaxEnemy;
                Wave_number++;
                OnNewWaveStart?.Invoke(Wave_number);
                MusicManagerResume?.Invoke();
                UpdateEnemyCount();
                UpdateWaveCount();
                
            }
        }
    }

    IEnumerator Enemydrop(List<int> enemyIndicesToSpawn)
    {
        ShuffleSpawnPoints(); // Shuffle the spawn points before spawning

        foreach (int index in enemyIndicesToSpawn)
        {
            Debug.Log("Spawning enemy at index: " + index);
            spawn(index);
            yield return new WaitForSeconds(SpawnTime);
        }
    }



    void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = UnityEngine.Random.Range(i, spawnPoints.Length);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }

  
    
    
    public void ShowFullscreen() => GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);

// Начался показ
    private void OnFullscreenStart()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        Debug.Log("ON FULLSCREEN START");
    }

    // Закончился показ
    private void OnFullscreenClose(bool success)
    {
        Time.timeScale=1f;
        AudioListener.volume=1f;
        Cursor.lockState = CursorLockMode.Locked;  // Sichqoncha kursorini markazga qotiradi va uni ekrandan yashiradi
        Cursor.visible = false; 
        Debug.Log("ON FULLSCREEN CLOSE");
    }
    public void ShowRewarded()
    {
       
        
       
        GP_Ads.ShowRewarded("", OnRewardedReward, OnRewardedStart, OnRewardedClose);
    }

    private void OnRewardedStart()
    {  
       
     
        AudioListener.volume=0f;
        Debug.Log("ON REWARDED: START");
    }


    // Reward is received
    private void OnRewardedReward(string value)
    {
       
        
       
    }

// Showing ended
    private void OnRewardedClose(bool success)
    { 
        Time.timeScale=1f;
        AudioListener.volume=1f;
        WaveWin.SetActive(false);
        moneyCount.Value += 150;
        timewave = Roundtime;
        StartCoroutine(TimeWave());
        Cursor.lockState = CursorLockMode.Locked;  // Sichqoncha kursorini markazga qotiradi va uni ekrandan yashiradi
        Cursor.visible = false;
        Debug.Log("ON REWARDED: CLOSE");
        Main_menu.Value = false;
    }

    private void OnEnable()
    {
        SaveEvent.OnRaised += SaveData;
    }

    private void OnDisable()
    {
        SaveEvent.OnRaised -= SaveData;
    }

    public void SaveData()
    {
        PlayerData data = SaveManager.instance.LoadPlayerData();
        data.currentWave = Wave_number;
        data.EnemyMax = MaxEnemy;
        data.moneycount= moneyCount.Value;
        SaveManager.instance.SavePlayerData(data);
    }


    public void LoadData()
    {
         PlayerData data = SaveManager.instance.LoadPlayerData();
         Wave_number=data.currentWave;
         MaxEnemy=data.EnemyMax;
         moneyCount.Value=data.moneycount;
        
        
    }
 
}
