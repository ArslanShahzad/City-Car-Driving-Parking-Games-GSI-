using NaveedPkg;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text statusText;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void GiveEarnedReward(string networkName)
    {
        statusText.text = networkName;
        if (MyGlobalValues.rewardType.Equals("skiplevel"))
            Debug.Log("skiplevel");
        else if (MyGlobalValues.rewardType.Equals("doublecoins"))
            Debug.Log("doublecoins");
        else if (MyGlobalValues.rewardType.Equals("doublecash"))
            Debug.Log("doublecash");
        else if (MyGlobalValues.rewardType.Equals("unlocklvl"))
            Debug.Log("unlocklvl");
        else if (MyGlobalValues.rewardType.Equals("unlockvehicle"))
            Debug.Log("unlockvehicle");
        else if (MyGlobalValues.rewardType.Equals("unlockvehicle"))
            Debug.Log("unlockvehicle");
    }

    public void ShowAdmobBanner()
    {
        AnalyticsManager.Instance.SimpleEvent("Show-Admob-Banner");
   //     MyAdsManager.Instance.ShowAdmobBanner();
    }

    public void RequestSmartBanner()
    {
     //   MyAdsManager.Instance.RequestAdmobBanner("bottom", "smartbanner");
    }

    public void HideAdmobBanner()
    {
        AnalyticsManager.Instance.GameplayEvent("Hide-Admob-Banner");
    //    MyAdsManager.Instance.HideAdmobBanner();
    }

    public void ShowAdmobRect()
    {
        AnalyticsManager.Instance.GameplayEvent("Show-Admob-Rect");
     //   MyAdsManager.Instance.ShowAdmobRect();
    }

    public void HideAdmobRect()
    {
        AnalyticsManager.Instance.GameplayEvent("Hide-Admob-Rect");
      //  MyAdsManager.Instance.HideAdmobRect();
    }

    public void ShowAdmobInterstitial()
    {
     //   MyAdsManager.Instance.ShowAdmobInterstitial();
    }

    public void ShowAdmobRewarded()
    {
     //   MyAdsManager.Instance.ShowAdmobRewardedVideo();
    }

    public void ShowUnityInterstitial()
    {
     //   MyAdsManager.Instance.ShowUnityInterstitial();
    }

    public void ShowUnityRewarded()
    {
      //  MyAdsManager.Instance.ShowUnityRewardedVideo();
    }

    ////////////////////////////////////
    public void ShowInterstitialAdmobElseUnity()
    {
     //   MyAdsManager.Instance.ShowInterstitialAdmobElseUnity();
    }

    public void ShowInterstitialUnityElseAdmob()
    {
     //   MyAdsManager.Instance.ShowInterstitialUnityElseAdmob();
    }

    ////////Rewarded Videos//////////////
    public void ShowRewardedAdmobElseUnity()
    {
     //   MyAdsManager.Instance.ShowRewardedAdmobElseUnity();
    }

    public void ShowRewardedUnityElseAdmob()
    {
    //    MyAdsManager.Instance.ShowRewardedUnityElseAdmob();
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Rateus()
    {
     //   MyAdsManager.Instance.RateUs();
    }

    public void RemoveAds()
    {
        //MyGlobalValues.isAdsBlock = true;
        PlayerPrefs.SetInt("RemoveAds", 1);
    }
    ///////////////////////////////////// 
}