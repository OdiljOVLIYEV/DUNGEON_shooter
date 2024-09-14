using System;
using System.Collections;
using System.Collections.Generic;
using GamePush;
using UnityEngine;

public class ADSTEST : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    { 
          ShowFullscreen();
 
    }

    
    // Show fullscreen
    public void ShowFullscreen() => GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);

// Showing started
    private void OnFullscreenStart()
    {
        
        AudioListener.volume=0f;
        Debug.Log("ON FULLSCREEN START");
    }

    // Showing ended
    private void OnFullscreenClose(bool success)
    {
     
        AudioListener.volume=1f;
        Debug.Log("ON FULLSCREEN CLOSE");
    }



}
