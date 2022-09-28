using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activeIndicator : MonoBehaviour
{

    public GameObject LeftLight;
    public bool isIndicatorWork = false;
    public static activeIndicator Instance;
    public static bool isLeftIndicator;
    public static bool isRightIndicator;
    public GameObject RightLight;

    public static bool IsBrake;
    public GameObject BrakeLight;


    public CarsController Controller;
    public GameObject LeftIndicatorTraining;
    public GameObject RightIndicatorTraining;
    public GameObject IntroductionPanel;
    public Text IntroductionText;

    public GameObject LeftIndicatorCamera;
    public GameObject RIghtIndicatorCamera;
    public GameObject MainCamera;

    public GameObject arrowDirection;

    public Transform Arrow;

    public GameObject ArrowDirection;

    public GameObject WrongWayPanel;
    public GameObject StopTheCarPanel;


    public AudioSource audioSource;
    public AudioClip checkpointClip;


    public int walingCharacterCounter = 0;
    public bool iswalking;
    // Start is called before the first frame update
    void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }

        //  CheckPointHandler
        audioSource = GetComponent<AudioSource>();
        Invoke("findcheckpoints",2f);
    }
    private void Update()
    {

        //if (ArrowDirection.activeInHierarchy && ArrowDirection)
        //{
        //    if (ArrowDirection)
        //    {

        //    Arrow.transform.LookAt(ArrowDirection.transform);
        //    }
        //}
        //else
        //{
        //    ArrowDirection = GameObject.Find("checkpoint");
        //}

        if (CarsController.instance.Brake)
        {
            
                BrakeLight.SetActive(true);
            
        }
        else
        {
            
                BrakeLight.SetActive(false);
            
        }
    }
    void findcheckpoints()
    {
        arrowDirection = GameObject.Find("CheckPointHandler");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "wrongway")
        {

            WrongWayPanel.SetActive(false);
            LevelManager.instance.ResetInfoImage();

        }
        if (other.gameObject.name == "StopPlace")
        {
            StopTheCarPanel.SetActive(false);
            LevelManager.instance.ResetInfoImage();
        }
        if (other.gameObject.name == "ConstrctionArea")
        {
            WrongWayPanel.SetActive(false);
            LevelManager.instance.ResetInfoImage();
        }
    }
    private void OnTriggerStay(Collider other)
    {
       if(other.gameObject.name == "StopPlace")
        {
            if(gameObject.GetComponent<VehicelController>().CurrentSpeed <= 0.1f)
            {
                LevelManager.instance.OnPointHandling(true);
                StartCoroutine(LevelManager.instance.resetValues());
             // print("Traffic Light");
                Color tempColor = Color.green;
                tempColor.a = 0.42f;
           
                other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", tempColor);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                
                StopTheCarPanel.SetActive(false);
                LevelManager.instance.ResetInfoImage();
            }
            else
            {
                StopTheCarPanel.SetActive(true);
                LevelManager.instance.OnPointHandling(false);
                StartCoroutine(LevelManager.instance.resetValues());
                LevelManager.instance.OnSignIndicator("stop");
                Invoke("RemovewarningPanels",2f);
            }
        }
        if (other.gameObject.name == "ConstrctionArea")
        {
         //   print("constructionArea");
            WrongWayPanel.SetActive(true);
            WrongWayPanel.GetComponent<Text>().text = "Construction Area";
          //  LevelManager.instance.OnPointHandling(false);
            StartCoroutine(LevelManager.instance.resetValues());
            LevelManager.instance.OnSignIndicator("slowSpeed");
        }
        if (other.gameObject.name == "overtaking")
        {
        //    print("constructionArea");
            WrongWayPanel.SetActive(true);
            WrongWayPanel.GetComponent<Text>().text = "Over Taking";
           // LevelManager.instance.OnPointHandling(false);
            StartCoroutine(LevelManager.instance.resetValues());
            LevelManager.instance.OnSignIndicator("overtaking");
        }


        if (other.gameObject.name == "Giveway")
        {
           // print("constructionArea");
            WrongWayPanel.SetActive(true);
            WrongWayPanel.GetComponent<Text>().text = "Give Way";
            // LevelManager.instance.OnPointHandling(false);
            StartCoroutine(LevelManager.instance.resetValues());
            LevelManager.instance.OnSignIndicator("Giveway");
        }
        if (other.gameObject.name == "StopTrafficPlace")
        {

            if (gameObject.GetComponent<VehicelController>().CurrentSpeed <= 0.1f)
            {
                LevelManager.instance.OnPointHandling(true);
                StartCoroutine(LevelManager.instance.resetValues());

                Color tempColor = Color.green;
                tempColor.a = 0.42f;
                other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", tempColor);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                
                LevelManager.instance.ResetInfoImage();
            }
            else
            {
                LevelManager.instance.OnSignIndicator("traffic");

            }
         
        }
        if (other.gameObject.name == "StopZebracrossing")
        {

            if (gameObject.GetComponent<VehicelController>().CurrentSpeed <= 0.1f)
            {
                LevelManager.instance.OnPointHandling(true);
                StartCoroutine(LevelManager.instance.resetValues());
                if (!iswalking)
                {
                    iswalking = true;
                    walkingGirl();
                }
                    Color tempColor = Color.green;
                tempColor.a = 0.42f;
                other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", tempColor);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                
                LevelManager.instance.ResetInfoImage();
            }
            else
            {
                LevelManager.instance.OnSignIndicator("zebracrossing");

            }

        }
    }


    void walkingGirl()
    {
        if (iswalking)
        {
            if (walingCharacterCounter == 0)
            {
                walingCharacterCounter++;
                iswalking = false;
                if (GameObject.Find("walkingGirl"))
                {
                    GameObject.Find("walkingGirl").GetComponent<Animator>().enabled = true;
                }
            }
            else if (walingCharacterCounter == 1)
            {
                walingCharacterCounter++;
                iswalking = false;
                if (GameObject.Find("SecondwalkingGirl"))
                {
                    GameObject.Find("SecondwalkingGirl").GetComponent<Animator>().enabled = true;
                }
            }

            Invoke("NotWaling", 3f);
        }
    }
    void NotWaling()
    {
        iswalking = true;
    }
    void RemovewarningPanels()
    {
        WrongWayPanel.SetActive(false);
        StopTheCarPanel.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftIndicatorCollider")
        {

            LeftIndicatorCamera.SetActive(true);
            MainCamera.SetActive(false);
            IntroductionPanel.SetActive(true);
            IntroductionText.text = "when turnning left, use indicator";
            LevelManager.instance.OnSignIndicator("left");
            Invoke("Resetall", 2f);

        }
        if (other.gameObject.name == "RightIndicatorCollider")
        {

            RIghtIndicatorCamera.SetActive(true);
            MainCamera.SetActive(false);
            IntroductionPanel.SetActive(true);
            IntroductionText.text = "when turnning Right, use indicator";
            LevelManager.instance.OnSignIndicator("right");
            Invoke("Resetall", 2f);

        }
        if (other.gameObject.name == "wrongway")
        {

            WrongWayPanel.SetActive(true);
            LevelManager.instance.OnPointHandling(false);
            StartCoroutine(LevelManager.instance.resetValues());
            LevelManager.instance.OnSignIndicator("wrong");
        }

        if (other.gameObject.name == "LeftIndicatorViolation")
        {
            if (!activeIndicator.isLeftIndicator)
            {
             //   WrongWayPanel.SetActive(true);

                LevelManager.instance.OnPointHandling(false);
                //IntroductionPanel.SetActive(true);
                //IntroductionText.text = "when turnning Right, use indicator";
                StartCoroutine(LevelManager.instance.resetValues());
            }
            else
            {
                LevelManager.instance.OnPointHandling(true);
                StartCoroutine(LevelManager.instance.resetValues());
            }
          
         //   LevelManager.instance.OnSignIndicator("wrong");
        }

        if (other.gameObject.name == "RightIndicatorViolation")
        {
            if (!activeIndicator.isRightIndicator)
            {
                //   WrongWayPanel.SetActive(true);

                LevelManager.instance.OnPointHandling(false);
                //IntroductionPanel.SetActive(true);
                //IntroductionText.text = "when turnning Right, use indicator";
                StartCoroutine(LevelManager.instance.resetValues());
            }
            else
            {
                LevelManager.instance.OnPointHandling(true);
                StartCoroutine(LevelManager.instance.resetValues());
            }

            //   LevelManager.instance.OnSignIndicator("wrong");
        }

        if (other.gameObject.name == "StopTrafficPlace")
        {
        LevelManager.instance.OnSignIndicator("traffic");   
        }

        if (other.gameObject.name == "LeftIndicator")
        {

            print("Left Indicator Active");
            activeIndicator.Instance.OnActiveTrainLeft();
            //  Time.timeScale = 0.0000001f;
        }
        if (other.gameObject.name == "RightIndicator")
        {

            print("Left Indicator Active");
            activeIndicator.Instance.OnActiveTrainRIght();
            //  Time.timeScale = 0.0000001f;
        }
        if (other.gameObject.name == "checkpoint")
        {
         //   audioSource.clip = checkpointClip;
            audioSource.PlayOneShot(checkpointClip);
           
            arrowDirection.GetComponent<ArrowDirection>().OnCHeckPOintCheck();
        }

    }

    private void Resetall()
    {
        Time.timeScale = 1f;
        LeftIndicatorCamera.SetActive(false);
        RIghtIndicatorCamera.SetActive(false);
        MainCamera.SetActive(true);
        IntroductionPanel.SetActive(false);
    }
    // Update is called once per frame

    public void OnLeftIndicator()
    {
      //  print("Left Indicator Work");
        if(isLeftIndicator && !isRightIndicator) { 
                  isIndicatorWork =! isIndicatorWork;
                  LeftLight.SetActive(isIndicatorWork);
                  Invoke("OnLeftIndicator",0.5f);
            }
    }
    public void OnRightIndicator()
    {
        isIndicatorWork = !isIndicatorWork;
        isRightIndicator = true;
        isLeftIndicator = false;
        RightLight.SetActive(isIndicatorWork);
        Invoke("OnRightIndicator", 0.5f);
    }
        
    public void OnBreakPress()
    {

    }
    public void OnReset()
    {
        CancelInvoke("OnRightIndicator");
        CancelInvoke("OnLeftIndicator");
        LeftLight.SetActive(false);
    
    }

    public void OnActiveTrainLeft()
    {
        // ControlAll.SetActive(false);
        LeftIndicatorTraining.SetActive(true);
        Controller.Direction = -1;
        GetComponent<VehicelController>().CurrentSpeed = 0f;
      
        Time.timeScale = 0.00001f;
    }

    public void OnActiveTrainRIght()
    {
        // ControlAll.SetActive(false);
        RightIndicatorTraining.SetActive(true);
        Controller.Direction = -1;
        GetComponent<VehicelController>().CurrentSpeed = 0f;

        Time.timeScale = 0.00001f;
    }
    public void OnStopLeftIndicator()
    {
        Time.timeScale = 1f;
        Controller.Direction = 1;
        LeftIndicatorTraining.SetActive(false);
        LevelManager.instance.OnPointHandling(true);
        StartCoroutine(LevelManager.instance.resetValues());
        LevelManager.instance.OnLeftIndicator();
    }
    public void OnStopRIghtIndicator()
    {
        Time.timeScale = 1f;
        Controller.Direction = 1;
        RightIndicatorTraining.SetActive(false);
        LevelManager.instance.OnPointHandling(true);
        StartCoroutine(LevelManager.instance.resetValues());
        LevelManager.instance.OnRightIndicator();
    }
}
