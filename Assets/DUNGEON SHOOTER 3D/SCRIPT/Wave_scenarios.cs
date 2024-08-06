using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave_scenarios : MonoBehaviour
{
    private Dictionary<int, Action> waveTasks;

    void Start()
    {
        waveTasks = new Dictionary<int, Action>
        {
            { 1, TaskForWave1 },
            { 2, TaskForWave2 },
            { 6, TaskForWave6 },
            // Add more tasks for additional waves as needed
        };

        Debug.Log("Wave_scenarios initialized.");
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
            DefaultTaskForWave();
        }
    }

    private void TaskForWave1()
    {
        Debug.Log("Task for Wave 1 executed!");
        // Your specific logic for wave 1
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
        Debug.Log("Default task for subsequent waves executed!");
        // Your default logic for other waves
    }
}