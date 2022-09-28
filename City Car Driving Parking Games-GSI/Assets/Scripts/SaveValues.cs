using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class SaveValues : MonoBehaviour
{
    private static SaveValues _instance;
    public static SaveValues instance { get { return _instance; } }
    public List<int> unlockLvl;
    public List<int> FailedLvl;
    public static bool isBackFromGameplay;
    public static bool isClassicMode, isModernMode, isChallengeMode;
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


  
}
