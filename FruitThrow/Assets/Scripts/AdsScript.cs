using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMode = true;

    public string gameID;
    public string adName;

    [SerializeField] private string androidAdUnitId = "5279826";
    [SerializeField] private string iOsAdUnitId = "5279827";
   [SerializeField] private string androidAd = "Interstitial_Android";
    [SerializeField] string iOsAd = "Interstitial_iOS";
    // private string _revardedVideo = "Rewarded_Android";
    // private string _banner = "Banner Android";
  
    void Awake()
    {
     
        adName = (Application.platform == RuntimePlatform.IPhonePlayer)
           ? iOsAd
           : androidAd;

        InitializeAds();
       
    }
    void Start()
    {


        //Advertisement.AddListener(this);


       // #region Banner

       // StartCoroutine(ShowBannerWhenInitialized());
       // Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

       // #endregion
    }
    public void InitializeAds()
    {
#if UNITY_IOS
            gameID = iOsAdUnitId;
#elif UNITY_ANDROID
        gameID = androidAdUnitId;
#elif UNITY_EDITOR
            _gameId = androidAdUnitId ; 
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode, this);
        }
    }
    public void LoadAd()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Loading Ad: " + adName);
            Advertisement.Load(adName, this);
        }
    }
    public void ShowAd()
    {
        
            Debug.Log("Showing Ad: " + adName);
            Advertisement.Show(adName, this); ;
        
       
    }


    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
       
    }

    public void OnUnityAdsAdLoaded(string placementId) { }
   

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adName} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adName}: {error.ToString()} - {message}");
    }


    public void OnUnityAdsShowStart(string placementId) { }
   
    public void OnUnityAdsShowClick(string placementId) { }


    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) 
    {

        Debug.Log(showCompletionState);
        Time.timeScale = 1;
    }
   

    public void OnInitializationComplete( )
    {
        Debug.Log("Unity Ads initialization complete.");

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
      
    }

   
}
