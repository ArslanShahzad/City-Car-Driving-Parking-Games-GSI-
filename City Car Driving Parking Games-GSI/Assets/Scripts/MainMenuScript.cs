using NaveedPkg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class MainMenuScript : MonoBehaviour
{
    public GameObject player;
    public GameObject removeAdBtn;

    public bool isAdLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        // print("CurrentFaileeeeeeee" + GameStats.Instance.FailedLevel);
        if (SaveValues.isBackFromGameplay || Splash.isBackFromGameplay)
        {
            PlayBtnClick();

        }
        else
        {
            MenuManager.instance.mainMenupanel.SetActive(true);
            MenuManager.instance.modeSelectionPanel.SetActive(false);


        }
        if (!Splash.isoneTime)
        {

            if (!PlayerPrefs.HasKey("CurrentUnlock"))
            {
                SaveValues.instance.unlockLvl.Add(0);
            }
            else
            {
                for (int i = 0; i < GameStats.Instance.UnlockLevel; i++)
                {

                    SaveValues.instance.unlockLvl.Add(PlayerPrefs.GetInt("SaveUnlockLvl" + i));
                }
                if (!SaveValues.instance.FailedLvl.Contains(0) && (GameStats.Instance.FailedLevel > 0))
                {
                    for (int i = 0; i < GameStats.Instance.FailedLevel; i++)
                    {
                        SaveValues.instance.FailedLvl.Add(PlayerPrefs.GetInt("SaveFailedLvl" + i));
                    }
                }
            }
            Splash.isoneTime = true;
        }
        if (PlayerPrefs.GetInt("RemoveAd") == 1)
        {
            removeAdBtn.SetActive(false);
        }


    }
    public void OnRewardedVideo()
    {
        AdScript.adScript.UserChoseToWatchAd();
    }
    public void PlayBtnClick()
    {
    //    MenuManager.instance.mainMenupanel.SetActive(true);

        MenuManager.instance.garagePanel.SetActive(true);
        MenuManager.instance.mainMenupanel.SetActive(false);
        // player.SetActive(false);
    }
    public void ExitbtnClick()
    {
        MenuManager.instance.ExitPannel.SetActive(true);

    }
    public void SettingBtnCick()
    {
        MenuManager.instance.settingPanel.SetActive(true);
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://gamesonicsinc.com/privacy-policy/");
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gsi.home.car.parking.game");
    }

    public void MoreGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8570829073560324764");
    }
}
