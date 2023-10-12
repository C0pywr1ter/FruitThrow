using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAd = "Rewarded_Android";
    [SerializeField] string iOsAd = "Rewarded_iOS";
    public string adName;
   // public string gameID;
   // [SerializeField] private string androidAdUnitId = "5279826";
    //[SerializeField] private string iOsAdUnitId = "5279827";


    [SerializeField] Button showAdButton;
    private PotScr potScr;
    void Start()
    {
        StartCoroutine(SearchForPot());

        adName = (Application.platform == RuntimePlatform.IPhonePlayer)
         ? iOsAd
         : androidAd;
        StartCoroutine(LoadAd());
    }
    private IEnumerator SearchForPot()
    {
        while(GameObject.FindGameObjectWithTag("Hoop") == null)
        {
            yield return new WaitForSeconds(0.5f);
        }
        potScr = GameObject.FindGameObjectWithTag("Hoop").GetComponent<PotScr>();
    }
    public IEnumerator LoadAd()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("Loading Ad: " + adName);
        Advertisement.Load(adName, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(adName))
        {
            
            showAdButton.onClick.AddListener(ShowAd);
          
            showAdButton.interactable = true;
       }
       
    }
    public void ShowAd()
    {

        showAdButton.interactable = false;
        Advertisement.Show(adName, this);
       
    }
    private IEnumerator WaitForNewAd()
    {
        yield return new WaitForSeconds(5);
        showAdButton.interactable = true;
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        StartCoroutine(WaitForNewAd());
        if (adUnitId.Equals(adName) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            potScr.skips = 5;
            // Grant a reward.
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
      
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        showAdButton.onClick.RemoveAllListeners();
    }
}
