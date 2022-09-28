using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaragePanel : MonoBehaviour
{
    public static int CarCounter = 0;

    public GameObject[] Cars;

    public GameObject RequiedTextPanel;
    public GameObject BuyBtn;
    public GameObject LockImage;
    public Text RequireTextCoin;
    public int CurrentCoins;
    public Text CurrentCoinsText;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Selected_" + 0, 1);
        PlayerPrefs.SetInt("CurrentCar", CarCounter);
        //    CurrentCoins = 100000;
        CurrentCoins = PlayerPrefs.GetInt("CurrentMoney");
        CurrentCoinsText.text = PlayerPrefs.GetInt("CurrentMoney").ToString();
        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].SetActive(false);
            Cars[CarCounter].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBack()
    {
        MenuManager.instance.mainMenupanel.SetActive(true);
        MenuManager.instance.garagePanel.SetActive(false);
    }

    public void onSelectCar()
    {
        MenuManager.instance.modeSelectionPanel.SetActive(true);
        MenuManager.instance.garagePanel.SetActive(false);
    }
    public void SelectedCarHandler(bool isNext)
    {
       
        if (isNext)
        {
            CarCounter++;
            if (CarCounter >= Cars.Length)
            {
                CarCounter = Cars.Length - 1;
            }
        }
        else
        {
            CarCounter--;
            if (CarCounter < 1)
            {
                CarCounter = 0;
            }
        }
        print(CarCounter + "   CarCounter");
        for (int i = 0; i < Cars.Length; i++)
        {
            Cars[i].SetActive(false);
            Cars[CarCounter].SetActive(true);
        }
        print("Selected Car Counter " + CarCounter);
        if (!PlayerPrefs.HasKey("Selected_" + CarCounter))
        {
            BuyBtn.SetActive(true);
            RequiedTextPanel.SetActive(true);
            LockImage.SetActive(true);
            RequireTextCoin.text = (CarCounter * 1000) + "";
        }
        else if (PlayerPrefs.HasKey("Selected_" + CarCounter))
        {
            BuyBtn.SetActive(false);
            LockImage.SetActive(false);
            RequiedTextPanel.SetActive(false);
            PlayerPrefs.SetInt("CurrentCar", CarCounter);
        }
    }

    public void BuyCar()
    {
        print("BuyCar :" + (CarCounter * 1000) + " : " + CurrentCoins);
        if (CurrentCoins >= CarCounter * 1000)
        {
            CurrentCoins -= CarCounter * 1000;
            PlayerPrefs.SetInt("Selected_" + CarCounter, 1);
            BuyBtn.SetActive(false);
            LockImage.SetActive(false);
            RequiedTextPanel.SetActive(false);
            PlayerPrefs.SetInt("CurrentMoney", CurrentCoins);
            CurrentCoinsText.text = "" + CurrentCoins;
            PlayerPrefs.SetInt("CurrentCar", CarCounter);
        }
        //else
        //{
        //    NotEnoughCoinsPanel.SetActive(true);
        //}
    }
    public void OnRewardedVideo()
    {
        AdScript.adScript.UserChoseToWatchAd();
    }
}
