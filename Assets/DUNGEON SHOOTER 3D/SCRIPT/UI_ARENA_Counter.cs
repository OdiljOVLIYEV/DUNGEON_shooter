using System;
using System.Collections;
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
    private int Wave_number;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public float MaxEnemy;
    private int SpawnEnemy;
    public float SpawnTime;
    [SerializeField] private FloatVariable KillEnemy_UI;
    public float timewave;

    private void Start()
    {
        MusicManagerStart?.Invoke();
        MusicManagerPause?.Invoke();   
      
        timewave = 4f;
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

    public void spawn()
    {
        if (enemies.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("Enemies or spawn points not set!");
            return;
        }

        // Iterate over spawn points and spawn enemies
        ShuffleSpawnPoints();

        // Choose a random enemy from the array
        GameObject enemyToSpawn = enemies[UnityEngine.Random.Range(0, enemies.Length)];

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
            timewave = 4f;
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
                MusicManagerResume?.Invoke();
                UpdateEnemyCount();
                UpdateWaveCount();
                StartCoroutine(Enemydrop());
            }
        }
    }

    IEnumerator Enemydrop()
    {
        while (SpawnEnemy > 0)
        {
            spawn();
            yield return new WaitForSeconds(SpawnTime);
            SpawnEnemy--;
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
