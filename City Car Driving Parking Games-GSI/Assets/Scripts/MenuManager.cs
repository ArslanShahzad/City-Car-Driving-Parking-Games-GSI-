using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaveedPkg;
using GoogleMobileAds.Api;
public class MenuManager : MonoBehaviour
{
    public GameObject levelSelectionpanel, mainMenupanel,ExitPannel,settingPanel,loading,modeSelectionPanel, garagePanel;
    public GameObject [] modeSelected;
    public static MenuManager instance;
    public static bool isAdLoad = false;
    public SoundManager soundManager;
    void Awake()
    {
        instance = this;
       
    }

    public void OnRewardedVideo()
    {
        AdScript.adScript.UserChoseToWatchAd();
    }

  
    private void Start()
    {
        SoundManager.Instance.bgmSource.volume = 1f;


        // print("First Ad Load --------adsasdasdas------------");

        //if (!isAdLoad)
        //{
        // isAdLoad = true;
        print("Start working");
          
       // }
        }
    public void ExitYes()
    {
        new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
    }

    public void OnGarage()
    {
        garagePanel.SetActive(true);
        mainMenupanel.SetActive(false);
    }
    public void ExitNo()
    {
        ExitPannel.SetActive(false);
    }
    public void ButtonClick()
    {
        SoundManager.Instance.ButtonClickSound();
    }
    public void RemoveAdsBtnClick()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }
}
