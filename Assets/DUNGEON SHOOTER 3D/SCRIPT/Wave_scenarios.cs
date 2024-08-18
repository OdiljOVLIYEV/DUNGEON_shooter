using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave_scenarios : MonoBehaviour
{
    private Dictionary<int, Action> waveTasks;
    
   
    private void Start()
    {
        waveTasks = new Dictionary<int, Action>
        {
            { 1, TaskForWave1 },
            { 2, TaskForWave2 },
            { 3, TaskForWave3 },
            { 4, TaskForWave4 },
            { 5, TaskForWave5 },
            { 6, TaskForWave6 },
            // Add more tasks for additional waves as needed
        };
    }

    private void OnEnable()
    {
        UI_ARENA_Counter.OnNewWaveStart += ExecuteWaveStartCommand;
        Debug.Log("Subscribed to OnNewWaveStart event.");
    }

    private void OnDisable()
    { 
        UI_ARENA_Counter.OnNewWaveStart -= ExecuteWaveStartCommand;
        Debug.Log("Unsubscribed from OnNewWaveStart event.");
    }

    private void ExecuteWaveStartCommand(int waveNumber)
    {
        
        Debug.Log($"Wave {waveNumber} started.");
        if (waveTasks.TryGetValue(waveNumber, out Action task))
        {
            task.Invoke();
        }
        else
        {
            if (waveNumber % 1 == 0)
            {
                SpawnBossEnemy();
            }
            else
            {
                
                DefaultTaskForWave();
            }
            
        }
        
        
    }
    
    

    private void TaskForWave1()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
            enemy_count.EnemyRank = 1;
            
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 0, 0 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }


    private void TaskForWave2()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
            enemy_count.EnemyRank = 1;
            
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 0, 0 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }

    private void TaskForWave3()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
           
            
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 2, 20 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
    private void TaskForWave4()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
           
            enemy_count.EnemyRank = 2;
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 0, 0 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
    private void TaskForWave5()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
           
            enemy_count.EnemyRank = 3;
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 0, 0 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
    private void TaskForWave6()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");
           
            enemy_count.EnemyRank = 3;
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 4, 3 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }

    private void DefaultTaskForWave()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();
        enemy_count.EnemyRank = 4;
        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");

            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 0, 0 }, // Requesting 11 enemies of index 0
            };

            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
    
    private void SpawnBossEnemy()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();
        enemy_count.EnemyRank = 4;
        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");

            // Specify the number of enemies for each index
            var manualEnemyCounts = new Dictionary<int, int>
            {
                { 6, 1 }, // Example: 5 enemies of index 3
                 // Example: 3 enemies of index 4
            };

            // Call the method with the dictionary of manual counts
            enemy_count.ReceiveWaveCommand(manualEnemyCounts);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
}