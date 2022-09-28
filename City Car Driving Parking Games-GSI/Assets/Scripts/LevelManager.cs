using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;
using NaveedPkg;
using NWH.VehiclePhysics;
using GoogleMobileAds.Api;
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levelObj;
    public GameObject[] Player;
    public GameObject lvlFailPanel, lvlCompletePanel, PausePanel, SettingPanel, controlPanel;
    public static LevelManager instance;
    public bool isManualSetLvl;
    public int manualLvlNo;
    public int[] reverseLvls;
    public static bool isreveseCheck;
    public AutoCam mainCamera;
    public Text SkiplvlTxt;
    public Transform reverseCamPos;
    public garageOrbit RotatCamera;
    public GameObject parkingPanelFill;
    public GameObject popUpforLvlComplete;
    public GameObject tutorialPanel, AllControlHide, levelCompleteEffect, RotateCamera;
    public GameObject lvlcompleteParticles;
    public Material GroundMaterial;
    public Texture[] GroundTextures;
    public GameObject hideButtons, removeAdBtn;
    public Button nextLvlBtn;
    public int[] lvlSuccessParticle;

    public Transform CarSetPosition;
    GameObject currentobj;

    public static bool isAdShow = false;

    public GameObject[] DrivingLevels;

    public Image INfoImage;
    public Sprite WrongwayImage;
    public Sprite LeftTurnImage;
    public Sprite RightTurnImage;
    public Sprite StopImage;
    public Sprite TrafficLightImage;
    public Sprite SlowSpeedImage;
    public Sprite overtakingImage;
    public Sprite GiveWayImage;
    public Sprite zebraCrossing;
    public GameObject plusPoints;
    public GameObject MinusPoints;
    private void Awake()
    {
        Player[GaragePanel.CarCounter].SetActive(true);
        instance = this;


        if (!LevelScriptHandler.isDrivingLevels)
        {
            if (Application.isEditor)
            {
                if (isManualSetLvl)
                {
                    GameStats.Instance.CurrentLevel = manualLvlNo;
                    GameStats.Instance.UnlockLevel = manualLvlNo;
                }
            }
            GameObject lvl = levelObj[GameStats.Instance.CurrentLevel];
            currentobj = Instantiate(lvl);
            if (currentobj.GetComponent<LevelDescription>())
                isreveseCheck = currentobj.GetComponent<LevelDescription>().isreveseCheck;
        }
        else
        {
            currentobj = DrivingLevels[PlayerPrefs.GetInt("DrivingLevel")] ;
            currentobj.SetActive(true);
            print("Driving Level Action");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelComplteCallBack();
        }

        if (!reverseCamPos)
        {
            reverseCamPos = GameObject.Find("reverseFollow").transform;
        }
    }

    public void OnTestingLevelComplete()
    {
        LevelComplteCallBack();
    }
    void Start()
    {
        PlayerPrefs.SetInt("AfterCamRotate", 1);
        PlayerPrefs.SetInt("SpeedActionDone", 1);
        PlayerPrefs.SetInt("SecondAction", 1);
        PlayerPrefs.SetInt("PressGearBox", 1);
        PlayerPrefs.SetInt("PressBreak", 1);
        Invoke("MoveCam", 7);
        //if (GameStats.Instance.CurrentLevel < 20)
        //{
        //    GroundMaterial.mainTexture = GroundTextures[0];
        //}
        //if (GameStats.Instance.CurrentLevel >= 20 && GameStats.Instance.CurrentLevel < 40)
        //{
        //    GroundMaterial.mainTexture = GroundTextures[1];
        //}
        //if (GameStats.Instance.CurrentLevel >= 40)
        //{
        //    GroundMaterial.mainTexture = GroundTextures[0];
        //}
        if (GameStats.Instance.CurrentLevel == 5)
        {
            controlPanel.SetActive(true);
        }

        CarSetPosition =  currentobj.transform.GetChild(0).transform;
        Player[GaragePanel.CarCounter].transform.position = currentobj.transform.GetChild(0).transform.position;
        Player[GaragePanel.CarCounter].transform.rotation = currentobj.transform.GetChild(0).transform.rotation;

   //     Firebase.Analytics.FirebaseAnalytics.LogEvent("level_start" + GameStats.Instance.CurrentLevel);
        if (GameStats.Instance.CurrentLevel == 3)
        {
            if (PlayerPrefs.GetInt("PressBreak") != 1)
            {
                currentobj.transform.GetChild(1).gameObject.SetActive(true);
                currentobj.transform.GetChild(2).gameObject.SetActive(false);
                currentobj.transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("IsClose", true);
                currentobj.transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("IsOpen", false);
            }
            else
            {
                currentobj.transform.GetChild(1).gameObject.SetActive(false);
                currentobj.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("AfterCamRotate") == 1)
        {
            tutorialPanel.SetActive(false);
            hideButtons.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(true);
            hideButtons.SetActive(false);
        }
        SteeringControl.wheelAngle = 0;
        if (PlayerPrefs.GetInt("RemoveAd") == 1)
        {
            removeAdBtn.SetActive(false);
        }

        AdScript.adScript.LoadBannerAD(AdSize.Banner, AdPosition.Top) ;
    }
    public void HurdleUpAnimation()
    {
        currentobj.transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
        currentobj.transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("IsClose", false);
    }
    public void MoveCam()
    {
        CancelInvoke("MoveCam");
        mainCamera.m_TurnSpeed = 0.25f;
    }
    public void FllowCam()
    {
        if (CarsController.instance.Direction == -1)
        {
            mainCamera.m_Target = reverseCamPos.transform;
        }
        else
        {
            mainCamera.m_Target = Player[PlayerPrefs.GetInt("cars")].transform;
        }
        // MoveCam();
    }
    bool isNextClick;
    public void LevelSuccessFulCalBack()
    {
        if (!LevelScriptHandler.isDrivingLevels)
        {
            if (GameStats.Instance.CurrentLevel < 119)
            {
                PlayerPrefs.SetInt("CurrentUnlock", GameStats.Instance.CurrentLevel);

                //if (GameStats.Instance.Tutorial == 0)
                //{
                //    GameStats.Instance.Tutorial = 1;
                //    LevelSuccessFulCalBack();
                //}

                if (!SaveValues.instance.unlockLvl.Contains(PlayerPrefs.GetInt("CurrentUnlock")))
                {
                    SaveValues.instance.unlockLvl.Add(PlayerPrefs.GetInt("CurrentUnlock"));
                    SaveValues.instance.FailedLvl.Remove(GameStats.Instance.CurrentLevel);
                    for (int i = 0; i < SaveValues.instance.FailedLvl.Count; i++)
                    {
                        PlayerPrefs.SetInt("SaveFailedLvl" + i, SaveValues.instance.FailedLvl[i]);
                    }
                    for (int i = 0; i < SaveValues.instance.unlockLvl.Count; i++)
                    {
                        PlayerPrefs.SetInt("SaveUnlockLvl" + i, SaveValues.instance.unlockLvl[i]);
                    }
                    GameStats.Instance.UnlockLevel += 1;
                    if (SaveValues.instance.FailedLvl.Count == 0)
                        GameStats.Instance.FailedLevel = 0;
                }
                GameStats.Instance.CurrentLevel += 1;
            }
            else
            {
                GameStats.Instance.CurrentLevel = 0;
                AdScript.adScript.RemoveBanner();
                SceneManager.LoadScene("GamePlay");
            }
        }
        else
        {
            print("Driving School Level COmplete =-----------------------------------------");
            int index = PlayerPrefs.GetInt("DrivingLevel");
            index++;
            print("Driving School Level Index =----------------------------------------- " + index);
            if (PlayerPrefs.GetInt("UnlockDrivingLevels") < index)
            {
                PlayerPrefs.SetInt("UnlockDrivingLevels",index);
            }
            
        }

    }
    public void NextLvlClick()
    { 


        isAdShow = false;
        AdScript.adScript.RemoveBanner();
        if (!LevelScriptHandler.isDrivingLevels)
        {
            if (GameStats.Instance.CurrentLevel < 120 && !isNextClick)
            {
                //if (GameStats.Instance.Tutorial == 0)
                //    GameStats.Instance.Tutorial = 1;
                //    LevelSuccessFulCalBack();
                if (GameStats.Instance.CurrentLevel == 40 && SaveValues.instance.unlockLvl.Count < 40)
                {
                    LevelManager.instance.popUpforLvlComplete.SetActive(true);
                    Invoke("GotoLvlSelection", 2.5f);
                }
                if (GameStats.Instance.CurrentLevel != 40 && GameStats.Instance.CurrentLevel < 40 && SaveValues.instance.unlockLvl.Count < 40)
                {
                    SceneManager.LoadScene("GamePlay");
                }

                if (GameStats.Instance.CurrentLevel != 40 && GameStats.Instance.CurrentLevel < 80 && SaveValues.instance.unlockLvl.Count < 80)
                {
                    SceneManager.LoadScene("GamePlay");

                }
                if (SaveValues.instance.unlockLvl.Count == 40)
                {
                    SceneManager.LoadScene("MainMenu");
                    SaveValues.isClassicMode = false;
                    SaveValues.isModernMode = false;
                    SaveValues.isChallengeMode = false;
                    Splash.isBackFromGameplay = true;
                    GameStats.Instance.FailedLevel = 0;
                    SaveValues.instance.FailedLvl.Clear();
                }
                if (GameStats.Instance.CurrentLevel == 80 && SaveValues.instance.unlockLvl.Count < 80)
                {
                    popUpforLvlComplete.SetActive(true);
                    Invoke("GotoLvlSelection", 2.5f);
                }
                if (SaveValues.instance.unlockLvl.Count == 80)
                {
                    SceneManager.LoadScene("MainMenu");
                    SaveValues.isClassicMode = false;
                    SaveValues.isModernMode = false;
                    SaveValues.isChallengeMode = false;
                    Splash.isBackFromGameplay = true;
                    GameStats.Instance.FailedLevel = 0;
                    SaveValues.instance.FailedLvl.Clear();
                }
                if (GameStats.Instance.CurrentLevel > 80)
                    SceneManager.LoadScene("GamePlay");
                if (SaveValues.instance.unlockLvl.Count >= 81)
                {
                    SceneManager.LoadScene("GamePlay");
                }
                isNextClick = true;
            }
        }
        else
        {
            int index = PlayerPrefs.GetInt("DrivingLevel");
            index++;
            if(index < DrivingLevels.Length)
            {
                PlayerPrefs.SetInt("DrivingLevel",index);
                SceneManager.LoadScene("GamePlay");
            }
            else
            {
                LevelScriptHandler.isDrivingLevelCompleted = true;
                SceneManager.LoadScene("MainMenu");
            }
            

        }
        
    }
    public void GotoHomeFromLevelComp()
    {
        isAdShow = false;
        Time.timeScale = 1;
        AdScript.adScript.RemoveBanner();
        SceneManager.LoadSceneAsync("MainMenu");
       // LevelSuccessFulCalBack();
        Splash.isBackFromGameplay = true;
    }
    void GotoLvlSelection()
    {
        popUpforLvlComplete.SetActive(false);
        SaveValues.isBackFromGameplay = true;
        AdScript.adScript.RemoveBanner();
        SceneManager.LoadScene("MainMenu");

    }
    public void RestartClick()
    {
        Time.timeScale = 1;
        AdScript.adScript.RemoveBanner();
        SceneManager.LoadSceneAsync("GamePlay");

    }
    public void MainMenuClick()
    {
        Time.timeScale = 1;
        AdScript.adScript.RemoveBanner();
        SceneManager.LoadSceneAsync("MainMenu");
        Splash.isBackFromGameplay = true;
    }
    public void SettingPanelClick()
    {
        AdScript.adScript.RemoveBanner();
        AdScript.adScript.LoadBannerAD(AdSize.Banner, AdPosition.TopRight);
        SettingPanel.SetActive(true);
        RotatCamera.enabled = false;

    }
    public void onBackFromSettingBtn()
    {
        AdScript.adScript.RemoveBanner();
        AdScript.adScript.LoadBannerAD(AdSize.MediumRectangle, AdPosition.TopLeft);
        SettingPanel.SetActive(false);
        RotatCamera.enabled = true;
    }
    public void PasueBtnClick()
    {
        RotatCamera.enabled = false;
        PausePanel.SetActive(true);
        AdScript.adScript.RemoveBanner();
        AdScript.adScript.LoadBannerAD(AdSize.MediumRectangle, AdPosition.TopLeft);

        Time.timeScale = 0;
        AdsCall();
    }
    public void ResumeBtn()
    {
        PausePanel.SetActive(false);
        RotatCamera.enabled = true;
        AdScript.adScript.RemoveBanner();
        AdScript.adScript.LoadBannerAD(AdSize.Banner, AdPosition.Top);
        Time.timeScale = 1;
    }
    public void SkipLevelClick()
    {
    }
    public void AfterRewardSkipLvl()
    {
        SkiplvlTxt.text = PlayerPrefs.GetInt("SkipCounter").ToString();
        if (GameStats.Instance.CurrentLevel < levelObj.Length - 1)
            GameStats.Instance.CurrentLevel += 1;
        SceneManager.LoadSceneAsync("GamePlay");
    }
    public void ButtonClick()
    {
        SoundManager.Instance.ButtonClickSound();
    }
    public void LevelComplteCallBack()
    {
        lvlCompletePanel.SetActive(true);
        hideButtons.SetActive(false);
        activeIndicator.Instance.IntroductionText.gameObject.SetActive(false);
        activeIndicator.Instance.IntroductionPanel.SetActive(false);
        activeIndicator.Instance.WrongWayPanel.SetActive(false);
          ResetInfoImage();
        levelCompleteEffect.SetActive(true);
        Player[PlayerPrefs.GetInt("cars")].SetActive(false);
        AllControlHide.SetActive(false);
        parkingPanelFill.SetActive(false);
        tutorialPanel.SetActive(false);
        LevelSuccessFulCalBack();
        if (GameStats.Instance.Tutorial == 0)
        {
            GameStats.Instance.Tutorial = 1;
         }

        int money = PlayerPrefs.GetInt("CurrentMoney");
        money += 500;

        PlayerPrefs.SetInt("CurrentMoney", money);
        LevelManager.instance.OnPointHandling(true);
        AdsCall();
        //if (GameStats.Instance.CurrentLevel == 5)
        //{
        //}
        //if (GameStats.Instance.CurrentLevel >= 2)
        //{
           
        //    //print("AdCallsssss");
        //}
    }
    void AdsCall()
    {
        AdScript.adScript.ShowInterstitial();
        AdScript.adScript.RemoveBanner();
        AdScript.adScript.LoadBannerAD(AdSize.MediumRectangle, AdPosition.TopLeft);

    }
    public void RemoveAdsBtnClick()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }

    public void OnLeftIndicator()
    {
        if (!activeIndicator.isLeftIndicator)
        {
            activeIndicator.Instance.OnReset();
            activeIndicator.isLeftIndicator = true;
            activeIndicator.isRightIndicator = false;
            activeIndicator.Instance.OnLeftIndicator();
        }
        else
        {
            activeIndicator.isLeftIndicator = false;
            activeIndicator.isRightIndicator = false;
            activeIndicator.Instance.OnReset();
        }
    }
    public void OnRightIndicator()
    {
        if (!activeIndicator.isRightIndicator)
        {
            activeIndicator.Instance.OnReset();
            activeIndicator.isLeftIndicator = false;
            activeIndicator.isRightIndicator = true;

            activeIndicator.Instance.OnRightIndicator();
        }
        else
        {
            activeIndicator.isLeftIndicator = false;
            activeIndicator.isRightIndicator = false;

            activeIndicator.Instance.OnReset();
        }

    }


    public void OnSignIndicator(string values)
    {
        switch (values)
        {
            case "wrong":
                INfoImage.sprite = WrongwayImage;
                INfoImage.gameObject.SetActive(true);
                
                break;
            case "left":
                INfoImage.sprite = LeftTurnImage;
                INfoImage.gameObject.SetActive(true);
                Invoke("ResetInfoImage", 4f);
                break;
            case "right":
                INfoImage.sprite = RightTurnImage;
                INfoImage.gameObject.SetActive(true);
                Invoke("ResetInfoImage", 4f);
                break;
            case "stop":
                INfoImage.sprite = StopImage;
                INfoImage.gameObject.SetActive(true);
                Invoke("ResetInfoImage", 4f);
                break;
            case "traffic":
                INfoImage.sprite = TrafficLightImage;
                INfoImage.gameObject.SetActive(true);
                Invoke("ResetInfoImage",4f);
                break;
            case "slowSpeed":
                INfoImage.sprite = SlowSpeedImage;
                INfoImage.gameObject.SetActive(true);
               // Invoke("ResetInfoImage", 4f);
                break;
            case "overtaking":
                INfoImage.sprite = overtakingImage;
                INfoImage.gameObject.SetActive(true);
                // Invoke("ResetInfoImage", 4f);
                break;
            case "zebracrossing":
                INfoImage.sprite = zebraCrossing;
                INfoImage.gameObject.SetActive(true);
                // Invoke("ResetInfoImage", 4f);
                break;
            case "Giveway":
                INfoImage.sprite = GiveWayImage;
                INfoImage.gameObject.SetActive(true);
                // Invoke("ResetInfoImage", 4f);
                break;
            default:
                break;
        }
    }


    public void OnPointHandling(bool value)
    {
     //   print("Values Handler ");
        if (value)
        {
            plusPoints.SetActive(true);
        }
        else
        {
            MinusPoints.SetActive(true);
        }
    }
    public void ResetInfoImage()
    {
        INfoImage.gameObject.SetActive(false);
     
       
     
    }

    public IEnumerator resetValues()
    {
        yield return new WaitForSeconds(2f);
        if (plusPoints.activeInHierarchy)
        {
            plusPoints.SetActive(false);
        }
        if (MinusPoints.activeInHierarchy)
        {
            MinusPoints.SetActive(false);
        }
    }
  
}
