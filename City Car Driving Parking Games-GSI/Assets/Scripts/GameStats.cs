using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    private void Awake()
    {
       
    }
    private void Update()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("currentLevel", 0); }
        set { PlayerPrefs.SetInt("currentLevel", value); }
    }
   
  
    public int UnlockLevel
    {
        get { return PlayerPrefs.GetInt("UnlockLevel", 0); }
        set { PlayerPrefs.SetInt("UnlockLevel", value); }
    }
    public int FailedLevel
    {
        get { return PlayerPrefs.GetInt("FailedLevel", 0); }
        set { PlayerPrefs.SetInt("FailedLevel", value); }
    }
    public int CoinCount
    {
        get { return PlayerPrefs.GetInt("CoinCount", 0); }
        set { PlayerPrefs.SetInt("CoinCount", value); }
    }
  
  
    public int CurrentCar
    {
        get { return PlayerPrefs.GetInt("CurrentCar", 0); }
        set { PlayerPrefs.SetInt("CurrentCar", value); }
    }
    public int Tutorial
    {
        get { return PlayerPrefs.GetInt("Tutorial", 0); }
        set { PlayerPrefs.SetInt("Tutorial", value); }
    }


}
