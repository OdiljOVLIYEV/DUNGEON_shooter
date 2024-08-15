using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using TMPro;

public class UI_ARENA_Counter : MonoBehaviour
{
    public TextMeshProUGUI EnemyCountText;
    public TextMeshProUGUI MoneyCountText;
    public TextMeshProUGUI WaveCountText;
    public TextMeshProUGUI TimeCountText;
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
    
    private void Start()
    {
        MusicManagerStart?.Invoke();
        MusicManagerPause?.Invoke();   
      
        timewave = Roundtime;
        StartCoroutine(TimeWave());
        WaveCountText.enabled = false;
        TimeCountText.enabled = true;
    }

    void Update()
    {
        if (MoneyCountText != null)
        {
            MoneyCountText.text = moneyCount.ToString() + " $";
        }

        if (timewave == 0)
        {
            UpdateEnemyCount();
        }
    }

    public void ReceiveWaveCommand(int[] manualIndices = null)
    {
        List<int> enemyIndicesToSpawn = new List<int>();

        // If manual indices are provided, add them to the spawn list
        if (manualIndices != null && manualIndices.Length > 0)
        {
            foreach (int index in manualIndices)
            {
                // Check if the index is within the valid range of the enemies array
                if (index >= 0 && index < enemies.Length)
                {
                    enemyIndicesToSpawn.Add(index);
                }
                else
                {
                    Debug.LogError("Invalid enemy index: " + index);
                }
            }
        }

        // Fill the rest of the spawn list with random indices from 1 to 3
        int remainingEnemies = (int)MaxEnemy - enemyIndicesToSpawn.Count;
        for (int i = 0; i < remainingEnemies; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, 4); // Adjust the range if needed
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

        // Ensure SpawnEnemy does not exceed the bounds of spawnPoints
        int spawnIndex = (SpawnEnemy - 1) % spawnPoints.Length;

        // Choose the current spawn point
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the enemy at the spawn point position and rotation
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }


    private void UpdateEnemyCount()
    {
        if (EnemyCountText != null)
        {
            EnemyCountText.text = KillEnemy_UI.ToString();
        }
        if (KillEnemy_UI == 0)
        {
            MusicManagerPause?.Invoke();            
            timewave = Roundtime;
            StartCoroutine(TimeWave());
        }
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
}
