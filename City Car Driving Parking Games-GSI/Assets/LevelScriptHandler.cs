using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class LevelScriptHandler : MonoBehaviour
{
    public GameObject LoadingPanel;

    public Button[] DrivingLevels;
    public Button[] parallelParkingLevels;
    public static bool isDrivingLevelCompleted;
    public GameObject ModeSelection;
    public static bool isAdsshow = false;

    public static int SelectedMode = 0;
    // Start is called before the first frame update

    void Start()
    {
        if (PlayerPrefs.GetInt("UnlockDrivingLevels") < 9)
        {
            for (int i = 0; i <= PlayerPrefs.GetInt("UnlockDrivingLevels"); i++)
            {
                DrivingLevels[i].interactable = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("UnlockDrivingLevels", 9);
            for (int i = 0; i <= PlayerPrefs.GetInt("UnlockDrivingLevels"); i++)
            {
                DrivingLevels[i].interactable = true;
            }
        }

        // ParallelParking Level
        if (PlayerPrefs.GetInt("UnlockParallelParkingLevels") < 4)
        {
            for (int i = 0; i <= PlayerPrefs.GetInt("UnlockParallelParkingLevels"); i++)
            {
                parallelParkingLevels[i].interactable = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("UnlockParallelParkingLevels", 4);
            for (int i = 0; i <= PlayerPrefs.GetInt("UnlockParallelParkingLevels"); i++)
            {
                parallelParkingLevels[i].interactable = true;
            }
        }



        if (isDrivingLevelCompleted)
        {
            ModeSelection.SetActive(true);
            isDrivingLevelCompleted = false;
        }
        else
        {
            ModeSelection.SetActive(false);
        }

        if (!isAdsshow)
        {
            print("Ads Show First Time");
          
            AdScript.adScript.LoadBannerAD(AdSize.Banner, AdPosition.Top);
            isAdsshow = true;
            Invoke("ResetAds",2f);
        }
    }

    void ResetAds()
    {
        isAdsshow = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnDrivingLevelSelection(int index)
    {
        SelectedMode = 0;
        PlayerPrefs.SetInt("DrivingLevel",index);
        AdScript.adScript.RemoveBanner();

        LoadingPanel.SetActive(true);
        SceneManager.LoadSceneAsync("GamePlay");
    }
    public void OnparallelLevelSelection(int index)
    {
        SelectedMode = 1;
        PlayerPrefs.SetInt("ParallelLevel", index);
        AdScript.adScript.RemoveBanner();

        LoadingPanel.SetActive(true);
        SceneManager.LoadSceneAsync("GamePlay");
    }
}
