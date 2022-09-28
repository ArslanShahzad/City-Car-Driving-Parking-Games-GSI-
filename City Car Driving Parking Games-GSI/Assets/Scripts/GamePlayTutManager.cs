using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTutManager : MonoBehaviour
{
    public GameObject[] ObjTutorial;
    public GameObject[] PanelTutorial;
    public float[] timer;
    public static GamePlayTutManager instance;
    public GameObject MainControl;
    public GameObject [] HidegearBoxActive;
    public bool isfirst, issecond, isthird, isfourth;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
        if (GameStats.Instance.CurrentLevel == 0 && PlayerPrefs.GetInt("SpeedActionDone") != 1)
        {
            ObjTutorial[1].SetActive(true);
            MainControl.SetActive(false);
        }
        if (GameStats.Instance.CurrentLevel == 1 && PlayerPrefs.GetInt("PressGearBox") != 1)
        {
            ShowGearBox();
            HidegearBoxActive[0].SetActive(false);
            HidegearBoxActive[1].SetActive(false);
        }
        if (GameStats.Instance.CurrentLevel == 2 || GameStats.Instance.CurrentLevel == 3)
        {
            ObjTutorial[1].SetActive(true);
            MainControl.SetActive(false);
            PlayerPrefs.SetInt("SpeedActionDone", 0);
        }
        if (GameStats.Instance.CurrentLevel == 4)
        {
            ObjTutorial[0].SetActive(true);
            MainControl.SetActive(false);
        }
    }
  
    public void SpeedActionCall()
    {
        if (!isfirst)
        {
            isfirst = true;
            Invoke("SpeedAction",1.35f);
        }
    }
    void SpeedAction()
    {
        for (int i = 0; i < ObjTutorial.Length; i++)
        {
            ObjTutorial[i].SetActive(false);
        }
        MainControl.SetActive(true);
        PlayerPrefs.SetInt("AfterCamRotate", 1);
        PlayerPrefs.SetInt("SpeedActionDone", 1);
        PlayerPrefs.SetInt("SecondAction", 1);
        PlayerPrefs.SetInt("PressGearBox", 1);
        PlayerPrefs.SetInt("PressBreak", 1);
       // LevelManager.instance.
      
    }
    public void AfterSpeedActionCall()
    {
        if (!issecond)
        {
            issecond = true;
            Invoke("AfterSpeedAction", 1.2f);
        }
    }
    void AfterSpeedAction()
    {
        for (int i = 0; i < PanelTutorial.Length; i++)
        {
            PanelTutorial[i].SetActive(false);
        }

        //PlayerPrefs.SetInt("SpeedActionDone", 1);
    }
    public void ShowGearBox()
    {
        if (!isfourth)
        {
            isfourth = true;
            Invoke("GearBoxAction", timer[2]);
        }
    }
    void GearBoxAction()
    {

        for (int i = 0; i < ObjTutorial.Length; i++)
        {
            ObjTutorial[i].SetActive(false);
        }
        ObjTutorial[3].SetActive(true);

    }
    public void ShowSteering()
    {
        if (!isfourth)
        {
            isfourth = true;
            Invoke("ShowSteeringAction", 0f);
            AfterSpeedAction();
            CarsController.instance.Accelerate = false;
        }
    }
    void ShowSteeringAction()
    {
        for (int i = 0; i < ObjTutorial.Length; i++)
        {
            ObjTutorial[i].SetActive(false);
        }
        ObjTutorial[2].SetActive(true);
    }
    public void PressSteering()
    {
        if (!isthird)
        {
            isthird = true;
            Invoke("PressSteeringAction", timer[2]);
        }
    }
    void PressSteeringAction()
    {
        //for (int i = 0; i < ObjTutorial.Length; i++)
        //{
        //    ObjTutorial[i].SetActive(false);
        //}
        MainControl.SetActive(false);
        HidegearBoxActive[2].SetActive(false);
        HidegearBoxActive[6].SetActive(false);
        HidegearBoxActive[3].SetActive(false);
        PlayerPrefs.SetInt("SpeedActionDone", 0);
        PanelTutorial[0].SetActive(true);
        ObjTutorial[1].SetActive(true);
        issecond = false;
    }
 
    public void PressGearBox()
    {
        if (!isthird)
        {
            isthird = true;
            Invoke("PressGearBoxAction", timer[2]);
        }
    }
    void PressGearBoxAction()
    {
        for (int i = 0; i < ObjTutorial.Length; i++)
        {
            ObjTutorial[i].SetActive(false);
        }
        HidegearBoxActive[7].SetActive(false);
        ObjTutorial[1].SetActive(true);
        PlayerPrefs.SetInt("SpeedActionDone", 0);
        //PlayerPrefs.SetInt("PressGearBox", 1);
    }
    public void ShowBreak()
    {
        if (!isfourth)
        {
            isfourth = true;
           
            Invoke("ShowBreakAction",0);
            
        }
    }
    void ShowBreakAction()
    {

        for (int i = 0; i < ObjTutorial.Length; i++)
        {
            ObjTutorial[i].SetActive(false);
        }
        ObjTutorial[4].SetActive(true);
        CarsController.instance.Brake = true;
    }
    public void PressBreak()
    {
        if (!isthird)
        {
            isthird = true;
            CarsController.instance.Brake = false;
            Invoke("PressBreakBoxAction", 0.15f);

            //CarsController.instance.Accelerate = false;
        }
    }
    void PressBreakBoxAction()
    {
        HidegearBoxActive[4].SetActive(false);
        HidegearBoxActive[5].SetActive(false);
        //PlayerPrefs.SetInt("PressBreak", 1);
        //CarsController.instance.Brake = false;
        Invoke("ShowAcceleration", 1.5f);
      
        if (PlayerPrefs.GetInt("PressBreak") != 1)
        {
            LevelManager.instance.HurdleUpAnimation();
        }
        CarsController.instance.Accelerate = false;
    }
    void ShowAcceleration()
    {
        PlayerPrefs.SetInt("SpeedActionDone", 0);
        PanelTutorial[0].SetActive(true);
        ObjTutorial[1].SetActive(true);
        issecond = false;
    }
}
