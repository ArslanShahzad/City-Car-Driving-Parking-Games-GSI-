using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class SettingPanel : MonoBehaviour
{
    public GameObject leftRightGlow, SteerGlow;
    public Slider MusicSlider, SfxSlider;
    void Start()
    {
        ClickChangeControl(PlayerPrefs.GetInt("ControlChange"));
    }
    public void ClickChangeControl(int i)
    {
        PlayerPrefs.SetInt("ControlChange", i);
        if (i == 0)
        {
            leftRightGlow.SetActive(true);
            SteerGlow.SetActive(false);
          
        }
        else
        {
            leftRightGlow.SetActive(false);
            SteerGlow.SetActive(true);
           
        }
        if (CarsController.instance)
        {
            CarsController.instance.ControlChange();
        }
    }
    public void BackBtnClick()
    {
        this.gameObject.SetActive(false);


        if (LevelManager.instance)
            LevelManager.instance.RotatCamera.enabled = false;
    }
    public void MusicSliderChangeFunc()
    {
        SoundManager.Instance.bgmSource.volume = MusicSlider.value;
    }
    public void SfxSliderChangeFunc()
    {
        SoundManager.Instance.sfxSource.volume = SfxSlider.value;
        SoundManager.Instance.specialSfxSource.volume = SfxSlider.value;
    }
}
