using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.UI;
using NWH.VehiclePhysics;
//using NaveedPkg;
using UnityEngine.SceneManagement;
//using SWS;
//using NaveedPkg;

public class VehicelController : MonoBehaviour
{
    public static VehicelController Instance;
    public float m_horizontalInput;
    private float m_verticalInput = 0;
    private float m_verticalInputbrake;
    private float m_steeringAngle;
    private float m_breakInput;
    public Transform COM;
    [Header("UI References")]
    public WheelCollider WheelColl_FR;
    public WheelCollider WheelColl_FL;
    public WheelCollider WheelColl_BR, WheelColl_BL;
    public Transform Wheel_FR, Wheel_FL;
    public Transform Wheel_BR, Wheel_BL;
    [Space]
    //public GameObject ReverseLights;
    //public GameObject BrakeLights;
    //public GameObject LeftLights;
    //public GameObject RightLights;
    //public GameObject HeadLights;
    //public GameObject OvertakeCollider;
    [Space]
    public Transform CamFollow;
    //  public List<Transform> PickCams;
    public GameObject HoodCam;
    [Header("Controller Settings")]
    [Space]
    public float MaxSteerAngle;
    public float MotorForce;
    public float BrakeForce;
    public float TurnForce;
    public float BackToNormalTurnForce;
    public float Drag; //This is the actual drag
    private float AppliedDrag; // this value will change
    [Space]
    public Transform Steering;
    public int gearNumber;
    [HideInInspector]
    public Buse CurrentBusControleSetting;
    [HideInInspector]
    public Rigidbody r_body;
    private float MaxMotorForce;
    private float MaxTurnForce;
    public float CurrentSpeed;
    public int MaxSpeed;
    [HideInInspector]
    public bool HitOnce;
    float WC_targetDistance;
    float WC_targetDistance2;
    JointSpring WC_suspensionSpring = new JointSpring();        //WC means WheelCollider, these are values are used for brake hydraulic suspension, these all are private
    private bool isBrakeSoundOn;
    public float suspensionDistance_temp;
    public int pickCamindex;
    int x;
    float steeringRot, pitch;
    int audioLerp = 6;
    [HideInInspector]
    public float steeringSensitivity;
    bool isGearChangedCompleted;
    public int RemainingCoins;
    public Animator playeranim;
    private bool isEnd;
    void Start()
    {
        Instance = this;
        HitOnce = false;
        isGearChangedCompleted = true;
        MaxTurnForce = TurnForce;
        MaxMotorForce = MotorForce;
        gameObject.AddComponent<AudioListener>();
        r_body = GetComponent<Rigidbody>();
        r_body.centerOfMass = COM.localPosition;
        AppliedDrag = 1f;
        WheelColl_FL.ConfigureVehicleSubsteps(55, 20, 30);
        WheelColl_BL.ConfigureVehicleSubsteps(55, 20, 30);
        WheelColl_FR.ConfigureVehicleSubsteps(55, 20, 30);
        WheelColl_BR.ConfigureVehicleSubsteps(55, 20, 30);
        WC_targetDistance2 = WheelColl_FL.suspensionSpring.targetPosition;
        steeringRot = Steering.localRotation.y;
        SetBusControleSetting();
        SoundManager.Instance.PlaySound(SoundManager.Instance.CarStart, false);
        Invoke("EngineSound", 1.2f);
        SoundManager.Instance.bgmSource.volume = 0.35f;
        // playeranim = GetComponent<Animator>();
    }
    void EngineSound()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.specialSfxSource, SoundManager.Instance.EngineSound);
    }
    void FixedUpdate()
    {
        if (HitOnce)
        {
            return;
        }
        CalculateSpeed();
        GetInput();
        Steer();
        UpdateWheelPoses();
    }

    private void CalculateSpeed()
    {
        //if (CarsController.instance.Fail_Panel.activeSelf)
        //    return; 
        CurrentSpeed = Mathf.Abs(r_body.velocity.magnitude * 10f);
        TurnForce = MaxTurnForce;
        SoundManager.Instance.specialSfxSource.volume = CurrentSpeed / 400;
        if (CurrentSpeed < CurrentBusControleSetting.Gears[gearNumber].speed)
        {
            if (gearNumber != 0)
            {
                gearNumber--;
            }
        }
        else if (CurrentSpeed > CurrentBusControleSetting.Gears[gearNumber].speed)
        {
            if (gearNumber != CurrentBusControleSetting.Gears.Length - 1)
            {
                gearNumber++;
                WC_targetDistance = WC_targetDistance2 - 0.1f;
                isGearChangedCompleted = false;
                Invoke("GearChangeDelay", 0.5f);
                //print("CurrentGear" + gearNumber);
            }
        }
        x = Mathf.Clamp(gearNumber, 0, CurrentBusControleSetting.Gears.Length - 1);

        MotorForce = CurrentBusControleSetting.Gears[gearNumber].gearTorque;
        if (CurrentSpeed > MaxSpeed)
        {
            MotorForce = 0;
        }
    }

    void GearChangeDelay()
    {
        isGearChangedCompleted = true;
    }
    #region Steer Methods
    public void Steer_By_PaddleControl()
    {
        if (CarsController.instance.Left || Input.GetKey(KeyCode.LeftArrow))
        {
            if (m_horizontalInput > -1)
                m_horizontalInput -= TurnForce / 100;
        }
        else if (CarsController.instance.Right || Input.GetKey(KeyCode.RightArrow))
        {
            if (m_horizontalInput < 1)
                m_horizontalInput += TurnForce / 100f;
        }
        else
        {
            if (m_horizontalInput > (BackToNormalTurnForce / 100))
                m_horizontalInput -= BackToNormalTurnForce / 100;
            else if (m_horizontalInput < -(BackToNormalTurnForce / 100))
                m_horizontalInput += BackToNormalTurnForce / 100;
            else
                m_horizontalInput = 0;
        }
        m_horizontalInput = m_horizontalInput * 1;
        Steering.localRotation = Quaternion.Euler(0, steeringRot + (m_horizontalInput * 180), 0);
    }

    public void Steer_By_SteeringWheel()
    {
        m_horizontalInput = SteeringControl.wheelAngle / 200;
        m_horizontalInput = m_horizontalInput * 1;
        Steering.localRotation = Quaternion.Euler(0, steeringRot + (m_horizontalInput * 530), 0);
    }

    public void Steer_By_Tilt()
    {
        float tilt_Val = Input.acceleration.x;
        if (tilt_Val < -0.005f || tilt_Val > 0.005f)
            m_horizontalInput = Mathf.Clamp(tilt_Val * 1.5f, -1, 1);
        else
            m_horizontalInput = 0;

        m_horizontalInput = m_horizontalInput * 1;
        Steering.localRotation = Quaternion.Euler(0, steeringRot + (m_horizontalInput * 180), 0);
    }

    #endregion

    public void GetInput()
    {
        if (isEnd)
            return;
        if (CarsController.instance.Brake || Input.GetKey(KeyCode.DownArrow))
        {
            if (CurrentSpeed < 4)
            {
                if (isBrakeSoundOn)
                {
                    isBrakeSoundOn = false;
                }
            }
            else
            {
                playeranim.SetBool("isNotBreak", false);
                playeranim.SetBool("isBreak", true);
                Invoke("ResetAnimtion", 1.2f);
                if (!isBrakeSoundOn)
                {
                    isBrakeSoundOn = true;
                }
            }
            //   CancelInvoke("ResetAnimtion");
           
            WheelColl_BR.brakeTorque = BrakeForce;
            WheelColl_BL.brakeTorque = BrakeForce;
            r_body.drag = Drag;
        }
       
        else if (CarsController.instance.Accelerate || Input.GetKey(KeyCode.UpArrow))
        {
           
            //playeranim.SetBool("isNotBreak", true);
            // playeranim.SetBool("isBreak", false);
            //if (CarsController.instance.Brake)
            //    return;
            //if (isGearChangedCompleted)
            //    WC_targetDistance = WC_targetDistance2 - 0.2f;
            r_body.drag = WheelColl_BR.brakeTorque = WheelColl_BL.brakeTorque = 0;
            //if (isBrakeSoundOn)
            //{
            //    isBrakeSoundOn = false;
            //}

            if (CarsController.instance.Direction == -1)
            {
                m_verticalInput = -1f;
            }
            else
            {
                m_verticalInput = 1f;
            }
            pitch = Mathf.Lerp(pitch, 1 + (0.15f * gearNumber) + (1.5f * 0.5f), Time.deltaTime * 10);
            SoundManager.Instance.specialSfxSource.pitch = pitch;
        }
        else
        {
            WC_targetDistance = WC_targetDistance2;
            if (isBrakeSoundOn)
            {
                isBrakeSoundOn = false;
            }
            r_body.drag = AppliedDrag;
            m_verticalInput = WheelColl_BR.brakeTorque = WheelColl_BL.brakeTorque = 0;
        }
        //***************************************************************************************************************************************
        if (CarsController.instance.AssignedControl == 1)
        {
            Steer_By_SteeringWheel();
        }
        else if (CarsController.instance.AssignedControl == 0)
        {
            Steer_By_PaddleControl();
        }
        else
        {
            Steer_By_Tilt();
        }
        WheelColl_BR.motorTorque = m_verticalInput * MotorForce;
        WheelColl_BL.motorTorque = m_verticalInput * MotorForce;
    }
    void ResetAnimtion()
    {
        playeranim.SetBool("isBreak", false);
        playeranim.SetBool("isNotBreak", true);
    }
    bool isoneTime;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("probe") && !LevelManager.instance.lvlCompletePanel.activeSelf && !isoneTime)
        {
            //  LevelManager.instance.lvlFailPanel.SetActive(true);
            Handheld.Vibrate();
            WheelColl_BR.brakeTorque = BrakeForce;
            WheelColl_BL.brakeTorque = BrakeForce;
            isoneTime = true;
            LevelManager.instance.SkiplvlTxt.text = PlayerPrefs.GetInt("SkipCounter").ToString();
            SoundManager.Instance.PlaySound(SoundManager.Instance.HitSound);
            LevelManager.instance.RotatCamera.enabled = false;
            Invoke("AdsCall", 1.2f);
            GameStats.Instance.FailedLevel++;
            if (!SaveValues.instance.FailedLvl.Contains(GameStats.Instance.CurrentLevel) && !SaveValues.instance.unlockLvl.Contains(GameStats.Instance.CurrentLevel))
            {
                SaveValues.instance.FailedLvl.Add(GameStats.Instance.CurrentLevel);
                for (int i = 0; i < SaveValues.instance.FailedLvl.Count; i++)
                {
                    PlayerPrefs.SetInt("SaveFailedLvl" + i, SaveValues.instance.FailedLvl[i]);
                }
            }
           // Firebase.Analytics.FirebaseAnalytics.LogEvent("level_fail" + GameStats.Instance.CurrentLevel);
            //print("level_fail" + GameStats.Instance.CurrentLevel);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finish"))
        {
            LevelFinish();

            //print("level_complete" + GameStats.Instance.CurrentLevel);
        }
        if (other.gameObject.CompareTag("SteerTrigger"))
        {
            if (GamePlayTutManager.instance && GameStats.Instance.CurrentLevel == 2 && PlayerPrefs.GetInt("SecondAction") != 1)
            {
                GamePlayTutManager.instance.ShowSteering();
                GamePlayTutManager.instance.MainControl.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("BreakTrigger"))
        {
            if (GamePlayTutManager.instance && GameStats.Instance.CurrentLevel == 3 && PlayerPrefs.GetInt("PressBreak") != 1)
            {
                GamePlayTutManager.instance.ShowBreak();

                GamePlayTutManager.instance.MainControl.SetActive(false);
            }
        }
    }
    void LevelFinish()
    {
        isEnd = true;
        CarsController.instance.Brake = true;
        CarsController.instance.Accelerate = false;
        r_body.drag = 5000;
        AppliedDrag = 500;
        WheelColl_BR.brakeTorque = 5000;
        WheelColl_BL.brakeTorque = 5000;
        SoundManager.Instance.PlaySound(SoundManager.Instance.levelcomp);
        if (LevelManager.instance)
        {
            //LevelManager.instance.RotatCamera.enabled = false;
            LevelManager.instance.parkingPanelFill.SetActive(true);
           // LevelManager.instance.RotateCamera.SetActive(true);
            for (int i = 0; i < LevelManager.instance.lvlSuccessParticle.Length; i++)
            {
                if (GameStats.Instance.CurrentLevel == LevelManager.instance.lvlSuccessParticle[i])
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver, false);
                    LevelManager.instance.lvlcompleteParticles.SetActive(true);
                    break;
                }

            }

            //LevelManager.instance.LevelSuccessFulCalBack();
        }
    //    Firebase.Analytics.FirebaseAnalytics.LogEvent("level_complete" + GameStats.Instance.CurrentLevel);

    }
    void AdsCall()
    {
        if (GameStats.Instance.CurrentLevel < 120)
            GameStats.Instance.CurrentLevel += 1;
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
            LevelManager.instance.popUpforLvlComplete.SetActive(true);
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
    }
    void GotoLvlSelection()
    {
        LevelManager.instance.popUpforLvlComplete.SetActive(false);
        SaveValues.isBackFromGameplay = true;
        SceneManager.LoadScene("MainMenu");

    }
    private void Steer()
    {
        m_steeringAngle = MaxSteerAngle * m_horizontalInput;
        WheelColl_FR.steerAngle = m_steeringAngle;
        WheelColl_FL.steerAngle = m_steeringAngle;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(WheelColl_FR, Wheel_FR);
        UpdateWheelPose(WheelColl_FL, Wheel_FL);
        UpdateWheelPose(WheelColl_BR, Wheel_BR);
        UpdateWheelPose(WheelColl_BL, Wheel_BL);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    void SetBusControleSetting()
    {
        CurrentBusControleSetting = BusManualSetting.Instance.GetCurrentSetting(0);
        BrakeForce = CurrentBusControleSetting.DefaultBrakeForce;
        BackToNormalTurnForce += CurrentBusControleSetting.BackToNormalTurnForce;
        TurnForce += CurrentBusControleSetting.TurnForce;
        MaxSpeed += CurrentBusControleSetting.UpgradedMaxSpeed;
    }
}

