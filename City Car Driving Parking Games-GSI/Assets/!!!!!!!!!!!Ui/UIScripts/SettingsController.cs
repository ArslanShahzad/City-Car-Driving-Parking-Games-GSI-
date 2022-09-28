using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider Sensitivity,Music,SFX;
    public Text MusicVolume, sfxVolume, sensitivityValue;
    public AudioSource aud;
    public AudioSource aud_BG;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void musicOnOff()
    {
        aud_BG.volume = Music.value;
        MusicVolume.text = ((int)(Music.value * 100)).ToString();
    }

    public void soundOnOff()
    {
        aud.volume = SFX.value;
        sfxVolume.text = ((int)(SFX.value * 100)).ToString();
    }

    public void OnSensitivityChanged()
    {
        PlayerPrefs.SetFloat("Sensitivity", Sensitivity.value);
    }
}
