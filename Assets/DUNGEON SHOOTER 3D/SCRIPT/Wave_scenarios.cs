using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave_scenarios : MonoBehaviour
{
    private Dictionary<int, Action> waveTasks;
    public UI_ARENA_Counter arenaCounter;
    void Start()
    {
        arenaCounter = GetComponent<UI_ARENA_Counter>();
        if (arenaCounter == null)
        {
            Debug.LogError("UI_ARENA_Counter object not found!");
            return;
        }
        waveTasks = new Dictionary<int, Action>
        {
            { 1, TaskForWave1 },
            { 2, TaskForWave2 },
            { 3, TaskForWave3 },
            // Add more tasks for additional waves as needed
        };

        Debug.Log("Wave_scenarios initialized.");
    }

    private void OnEnable()
    {
       // UI_ARENA_Counter.OnNewWaveStart += ExecuteWaveStartCommand;
        Debug.Log("Subscribed to OnNewWaveStart event.");
    }

    private void OnDisable()
    {
       // UI_ARENA_Counter.OnNewWaveStart -= ExecuteWaveStartCommand;
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
            DefaultTaskForWave();
        }
    }

    private void TaskForWave1()
    {
        SpawnEnemiesByIndex(arenaCounter, new Dictionary<int, int> { { 0, 3 } });
    }

    private void TaskForWave2()
    {
        SpawnEnemiesByIndex(arenaCounter, new Dictionary<int, int> { { 0, 6 } }); // 3 ta dushman turini 0 indeksidan va 2 ta dushman turini 1 indeksidan spawn qilish
    }

    private void TaskForWave3()
    {
        SpawnEnemiesByIndex(arenaCounter, new Dictionary<int, int> { { 0, 4 }, { 1, 5 } }); // 10 ta dushman turini 1 indeksidan va 5 ta dushman turini 2 indeksidan spawn qilish
    }

    private void SpawnEnemiesByIndex(UI_ARENA_Counter counter, Dictionary<int, int> spawnCounts)
    {
        foreach (var entry in spawnCounts)
        {
            int enemyIndex = entry.Key;
            int count = entry.Value;
            for (int i = 0; i < count; i++)
            {
                counter.spawn(); // Bir marta spawn qilish
            }
        }
    }
    private void DefaultTaskForWave()
    {
        Debug.Log("Default task for subsequent waves executed!");
        // Your default logic for other waves
    }
}