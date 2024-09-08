using GamePush;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private Button showFullscreenButton;
    [SerializeField] private Button showPreloaderButton;
    [SerializeField] private Button showRewardedButton;

    private void OnEnable()
    {
        showFullscreenButton.onClick.AddListener(ShowFullscreen);
        showRewardedButton.onClick.AddListener(ShowRewarded);
        showPreloaderButton.onClick.AddListener(ShowPreloader);
    }

    private void OnDisable()
    {
        showFullscreenButton.onClick.RemoveListener(ShowFullscreen);
        showRewardedButton.onClick.RemoveListener(ShowRewarded);
        showPreloaderButton.onClick.RemoveListener(ShowPreloader);
    }

    public void ShowFullscreen()
    {
        if(GP_Ads.IsFullscreenAvailable()) // Fullscreen borligini tekshirish
            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
    }

    public void ShowPreloader()
    {
        if(GP_Ads.IsPreloaderAvailable()) // Preloader borligini tekshirish
            GP_Ads.ShowPreloader(OnPreloaderStart, OnPreloaderClose);
    }

    public void ShowRewarded()
    {
        if(GP_Ads.IsRewardedAvailable()) // Reward reklamani borligini tekshirish
            GP_Ads.ShowRewarded("COINS", OnRewardedReward, OnRewardedStart, OnRewardedClose);
    } 
    
    /// <summary>
    /// ON FULLSCREEN START/CLOSE
    /// </summary>
    private void OnFullscreenStart() => Debug.Log("ON FULLSCREEN START");
    private void OnFullscreenClose(bool success) => Debug.Log("ON FULLSCREEN CLOSE");
    /// <summary>
    /// ON PRELOADER START/CLOSE
    /// </summary>
    private void OnPreloaderStart() => Debug.Log("ON PRELOADER: START");
    private void OnPreloaderClose(bool success) => Debug.Log("ON PRELOADER: CLOSE");
    /// <summary>
    /// ON REWARDED START/CLOSE
    /// </summary>
    private void OnRewardedStart() => Debug.Log("ON REWARDED: START");
    private void OnRewardedReward(string value)
    {
        if (value == "COINS")
            Debug.Log("ON REWARDED: +150 COINS");
    }
    private void OnRewardedClose(bool success) => Debug.Log("ON REWARDED: CLOSE");
}
