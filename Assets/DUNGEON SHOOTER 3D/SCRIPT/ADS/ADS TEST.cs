using System;
using System.Collections;
using System.Collections.Generic;
using GamePush;
using UnityEngine;

public class ADSTEST : MonoBehaviour
{
    
    private void Start()
    { 
      
          ShowFullscreen();
          
         // ShowSticky();
      
    
      
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


    // Показать sticky баннер, дальше автообновляется сам
    public void ShowSticky() => GP_Ads.ShowSticky();
// Обновить sticky баннер, принудительное обновление
    public void RefreshSticky() => GP_Ads.RefreshSticky();
// Закрыть sticky баннер
    public void CloseSticky() => GP_Ads.CloseSticky();

// Открылся баннер
    private void OnStickyStart() => Debug.Log("ON STICKY: START");
// Закрылся баннер
    private void OnStickyClose() => Debug.Log("ON STICKY: CLOSE");
// Баннер показался на экране
    private void OnStickyRender() => Debug.Log("ON STICKY: RENDER");
// Баннер обновился
    private void OnStickyRefresh() => Debug.Log("ON STICKY: REFRESH");

}
