using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaveedPkg;
public class Splash : MonoBehaviour
{
    private static Splash _instance;
    public static Splash Instance { get { return _instance; } }
    public static bool isBackFromGameplay,isoneTime;
    public static int Controltype;
   // public static bool isAdsBlock, isClassicMode, isModernMode, isChallengeMode;
    public int[] totalunlockLvl;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        Invoke("NextScene", 2.5f);
        if (AnalyticsManager.Instance)
            AnalyticsManager.Instance.SimpleEvent("GameStart");
        //if (SystemInfo.systemMemorySize <= 1024)
        //{
        //    QualitySettings.SetQualityLevel(0, true);
        //    QualitySettings.masterTextureLimit = 1;
        //}
        //if (SystemInfo.systemMemorySize > 1024)
        //{
        //    QualitySettings.SetQualityLevel(1, true);
        //    QualitySettings.masterTextureLimit = 0;
        //}
        Application.targetFrameRate = 60;

    }
    void NextScene()
    {

        SceneManager.LoadSceneAsync("MainMenu");
    }

}
