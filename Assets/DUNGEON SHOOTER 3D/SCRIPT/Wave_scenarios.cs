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
            //{ 1, TaskForWave1 },
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
            if (waveNumber % 10 == 0)
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
        // Try to get the UI_ARENA_Counter component from the GameObject
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");

            // Specify enemies manually (ensure these indices are within bounds)
            int[] manualIndices = new int[] { 5, 1 }; // Example indices; adjust as needed

            // Call the method with the manual indices
            enemy_count.ReceiveWaveCommand(manualIndices);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }





    private void TaskForWave2()
    {
        Debug.Log("Task for Wave 2 executed!");
        // Your specific logic for wave 2
    }

    private void TaskForWave6()
    {
        Debug.Log("Task for Wave 6 executed!");
        // Your specific logic for wave 6
    }

    private void DefaultTaskForWave()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");

            // Specify enemies manually (ensure these indices are within bounds)
           // int[] manualIndices = new int[] { 0, 0 }; // Example indices; adjust as needed

            // Call the method with the manual indices
            enemy_count.ReceiveWaveCommand();
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
    
    private void SpawnBossEnemy()
    {
        UI_ARENA_Counter enemy_count = GetComponent<UI_ARENA_Counter>();

        if (enemy_count != null)
        {
            Debug.Log("Task for Wave 1 executed!");

            // Specify enemies manually (ensure these indices are within bounds)
            int[] manualIndices = new int[] { 6, 1 }; // Example indices; adjust as needed

            // Call the method with the manual indices
            enemy_count.ReceiveWaveCommand(manualIndices);
        }
        else
        {
            Debug.LogError("UI_ARENA_Counter component not found on this GameObject!");
        }
    }
}