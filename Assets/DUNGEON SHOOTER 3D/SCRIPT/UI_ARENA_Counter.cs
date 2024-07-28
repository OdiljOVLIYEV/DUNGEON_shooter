using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI; // Eğer bir UI Text elemanına sahipseniz
using TMPro;

public class UI_ARENA_Counter : MonoBehaviour
{
    public TextMeshProUGUI EnemyCountText; // UI Text elemanı sahnedeki düşman sayısını göstermek için
    public TextMeshProUGUI MoneyCountText;
    public TextMeshProUGUI WaveCountText;
    public int NextWaveAddEnemyCounter;
    [SerializeField] private IntVariable moneyCount;
    private int Wave_number;
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float MaxEnemy;
    private float SpawnEnemy;
    [SerializeField] private FloatVariable KillEnemy_UI;
    //private float SpawnEnemy;
    public float timespawn;
   
    private void Start()
    {
       
        Wave_number = 0;
        UpdateWaveCount();
    }

    void Update()
    {
    
        //UpdateDestroyCount();
        
        if (MoneyCountText != null)
        {
            MoneyCountText.text = moneyCount.ToString() + " $";
        }

        // "Enemy" etiketi ile sahnedeki tüm nesneleri say
          GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         int enemyCount = enemies.Length;
        
        if (enemyCount == 0)
        {               
            
            MaxEnemy += NextWaveAddEnemyCounter;
            SpawnEnemy=MaxEnemy;
            KillEnemy_UI.Value = MaxEnemy;
            StartCoroutine(Enemydrop());
            Wave_number++;
            UpdateWaveCount();
            UpdateEnemyCount();
          
            // UI Text elemanına düşman sayısını yazdır

        }
        UpdateEnemyCount();
        
       
        
      
        
        
    }

    public void spawn()
    {
        Vector3 spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
    private void UpdateEnemyCount()
    {
        
        if (EnemyCountText != null)
        {
            EnemyCountText.text =KillEnemy_UI.ToString();
        }
    }
   /* private void UpdateDestroyCount()
    {
        if (EnemyCountText != null)
        {
            EnemyCountText.text = "ENEMIES: " + enemyCount.ToString();
        }
    }*/
    private void UpdateWaveCount()
    {
        if (WaveCountText != null)
        {
            WaveCountText.text = "Wave " + Wave_number.ToString();
        }
    }
    
    IEnumerator Enemydrop()
    {

        while (SpawnEnemy > 0)
        {

            
            spawn();
            yield return new WaitForSeconds(timespawn);
            SpawnEnemy -= 1;
            
        }

        



    }
}