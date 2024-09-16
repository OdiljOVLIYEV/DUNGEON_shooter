using UnityEngine;
using GamePush;

public class Game : MonoBehaviour
{
    private bool isPluginReady = false; // Bayroq
    
    private async void Start()
    {
        await GP_Init.Ready;
        OnPluginReady();
    }

    private void CheckReady()
    {
        if (GP_Init.isReady && !isPluginReady) // Agar plagin tayyor va hali chaqirilmagan bo'lsa
        {
            OnPluginReady();
        }
    }

    private void OnPluginReady()
    {
        if (!isPluginReady) // Faqat bir marta chaqirilishi uchun
        {
            isPluginReady = true;
            Debug.Log("Plugin ready");
            GP_Game.GameReady();
            ShowFullscreen();
        }
    }

    public void GameReady()
    {
      
       
        Debug.Log("GAME: READY");
    }

    public void ShowFullscreen() => GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);

    private void OnFullscreenStart()
    {
        AudioListener.volume = 0f;
        Debug.Log("ON FULLSCREEN START");
    }

    private void OnFullscreenClose(bool success)
    {
        AudioListener.volume = 1f;
        Debug.Log("ON FULLSCREEN CLOSE");
    }
}