using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class BannerAd : MonoBehaviour
{
    [SerializeField] private string androidAd = "Interstitial_Android";
    [SerializeField] string iOsAd = "Interstitial_iOS";
    public string adName;
    [SerializeField] Button showBannerButton;
    void Start()
    {
        adName = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? iOsAd
         : androidAd;
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
       StartCoroutine( LoadBanner());
    }
    public IEnumerator LoadBanner()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

      
        Advertisement.Banner.Load(adName, options);
    }
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

      
        showBannerButton.onClick.AddListener(ShowBannerAd);
     
        showBannerButton.interactable = true;
      
    }

  
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
       
    }

    void ShowBannerAd()
    {
        showBannerButton.onClick.AddListener(HideBannerAd);
        showBannerButton.interactable = true;
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

  
        Advertisement.Banner.Show(adName, options);
    }


    void HideBannerAd()
    {
        showBannerButton.onClick.AddListener(ShowBannerAd);

        showBannerButton.interactable = true;
        
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        // Clean up the listeners:
    
        showBannerButton.onClick.RemoveAllListeners();
     
    }
}
