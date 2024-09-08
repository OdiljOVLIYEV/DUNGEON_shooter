using System;
using System.Collections;
using System.Collections.Generic;
using GamePush;
using Obvious.Soap;
using UnityEngine;

public class REWARDEDADS : MonoBehaviour
{
    public GameObject btn1;
    public GameObject btn2;
    [SerializeField] private BoolVariable ContinueButton; 
    private void Start()
    {
        ContinueButton.Value = false;
        
    }

    private void Update()
    {
        if (ContinueButton == false)
        {
            ContinueButton.Value = true;
            btn1.SetActive(true);
            btn2.SetActive(false);
            
        }
    }

    public void AdsCall()
    {
       
        ShowRewarded();
        
    }
    // Update is called once per frame
    public void ShowRewarded() => GP_Ads.ShowRewarded("", OnRewardedReward, OnRewardedStart, OnRewardedClose);


// Showing started
    private void OnRewardedStart()
    {
        Time.timeScale=0f;
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
        btn1.SetActive(false);
        btn2.SetActive(true);
        Time.timeScale=1f;
        AudioListener.volume=1f;
       
        Debug.Log("ON REWARDED: CLOSE");
    }

    
}
