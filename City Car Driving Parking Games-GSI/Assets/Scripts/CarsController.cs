using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

public class CarsController : MonoBehaviour
{
    public static CarsController instance;
    public bool Accelerate, Left, Right, Brake;
    public int Direction, AssignedControl;
    public GameObject Forward_button, Backward_button;
    public GameObject[] Controls;
    private int direction_temp = 1;
    private int currentControl;
    public GameObject player;
    public Text currentlvl;
    public GameObject reverseCamera, frontCamera;
    Vector3 startPos;
    Quaternion startRot;
    public
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < Controls.Length; i++)
        {
            if (AssignedControl == i)
            {
                Controls[i].SetActive(true);
            }
            else
            {
                Controls[i].SetActive(false);
            }
        }
        startPos = player.transform.position;
        startRot = player.transform.rotation;

        if (LevelScriptHandler.SelectedMode == 0)
        {
            int currentobj = PlayerPrefs.GetInt("DrivingLevel");
            currentobj++;
            currentlvl.text = "LEVEL : " + currentobj.ToString();
        }
        else if (LevelScriptHandler.SelectedMode == 1)
        {
           int currentobj = PlayerPrefs.GetInt("ParallelLevel");
            currentobj++;
            currentlvl.text = "LEVEL : " + currentobj.ToString();
        }
        else if (LevelScriptHandler.SelectedMode == 2)
        {
            int lvlno = GameStats.Instance.CurrentLevel + 1;
            currentlvl.text = "LEVEL : " + lvlno.ToString();
        }

        if (LevelManager.isreveseCheck)
        {
            Direction = -1;
            Forward_button.SetActive(false);
            Backward_button.SetActive(true);
         
        }
        else
        {
            Direction = 1;
            Forward_button.SetActive(true);
            Backward_button.SetActive(false);
            // frontCamera.SetActive(true);
            // reverseCamera.SetActive(false);
        }
        LevelManager.instance.FllowCam();
        ControlChange();
    }
    public void ControlChange()
    {
        for (int i = 0; i < Controls.Length; i++)
        {
            Controls[i].SetActive(false);
        }
        Controls[PlayerPrefs.GetInt("ControlChange")].SetActive(true);
        AssignedControl = PlayerPrefs.GetInt("ControlChange");

    }
    public void ToogleAccelerate(bool flag)
    {
        Accelerate = flag;
        if (GamePlayTutManager.instance && PlayerPrefs.GetInt("SpeedActionDone") !=1)
        {
            if (Accelerate)
                GamePlayTutManager.instance.AfterSpeedActionCall();
        }
       
    }
    public void ToogleBrake(bool flag)
    {
        Brake = flag;
     
        if (GameStats.Instance.CurrentLevel == 3 && PlayerPrefs.GetInt("PressBreak") != 1)
        {
            if (Brake)
                GamePlayTutManager.instance.PressBreak();
        }
    }

    public void Toogleft(bool flag)
    {
        Left = flag;
    }

    public void ToogleRight(bool flag)
    {
        Right = flag;
    }
    public void ChangeDirection()
    {
        direction_temp *= -1;
        Direction = direction_temp;
        if (Direction == 1)
        {
            Forward_button.SetActive(true);
            Backward_button.SetActive(false);
            //reverseCamera.SetActive(false);
            //frontCamera.SetActive(true);
        }
        else
        {
            Forward_button.SetActive(false);
            Backward_button.SetActive(true);
            //reverseCamera.SetActive(true);
            //frontCamera.SetActive(false);
        }
        if (LevelManager.instance)
        {
            LevelManager.instance.FllowCam();
        }
       if (GameStats.Instance.CurrentLevel == 1 && PlayerPrefs.GetInt("PressGearBox") !=1)
        {
            GamePlayTutManager.instance.PressGearBox();
        }
            LevelManager.instance.mainCamera.m_TurnSpeed = 1;
        Invoke("ResetCam", 3f);
    }
    void ResetCam()
    {
        //LevelManager.instance.mainCamera.m_UpdateType = AbstractTargetFollower.UpdateType.FixedUpdate;
        LevelManager.instance.mainCamera.m_TurnSpeed = 0.25f;
    }
    public void ResetPosiotion()
    {
        player.transform.position = startPos;
        player.transform.rotation = startRot;
    }

}
