using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModeSelection : MonoBehaviour
{
    public GameObject player;
    public Button[] modeLock;

    public GameObject DrivingModePanel;
    private void Start()
    {
        //  print("CurrentLevel"+g)
        if (SaveValues.instance.unlockLvl.Count >= 40)
        {
            modeLock[0].interactable = true;
            modeLock[0].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (SaveValues.instance.unlockLvl.Count >= 80)
        {
            modeLock[0].interactable = true;
            modeLock[1].interactable = true;
            modeLock[0].transform.GetChild(0).gameObject.SetActive(false);
            modeLock[1].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ClickOnMode(int current)
    {
        if (current == 0)
        {
            DrivingModePanel.SetActive(true);
            SaveValues.isClassicMode = false;
            SaveValues.isModernMode = false;
            SaveValues.isChallengeMode = false;
            LevelScriptHandler.isDrivingLevels = true;
        }
        else if (current == 1)
        {
            SaveValues.isClassicMode = true;
            SaveValues.isModernMode = false;
            SaveValues.isChallengeMode = false;
            LevelScriptHandler.isDrivingLevels = false;

        }
        else if (current == 2)
        {
            SaveValues.isClassicMode = false;
            SaveValues.isModernMode = true;
            SaveValues.isChallengeMode = false;
            LevelScriptHandler.isDrivingLevels = false;
        }
        else if (current == 3)
        {
            SaveValues.isClassicMode = false;
            SaveValues.isModernMode = false;
            SaveValues.isChallengeMode = true;
            LevelScriptHandler.isDrivingLevels = false;
        }

        print("MenuManager.instance.modeSelected.Length     + " + MenuManager.instance.modeSelected.Length);
        for (int i = 0; i < MenuManager.instance.modeSelected.Length; i++)
        {
            MenuManager.instance.modeSelected[i].SetActive(false);

        }
        if (SaveValues.isClassicMode)
        {
            MenuManager.instance.modeSelected[0].SetActive(true);
        }
        else if (SaveValues.isModernMode)
        {
            MenuManager.instance.modeSelected[1].SetActive(true);
        }
        else if (SaveValues.isChallengeMode)
        {
            MenuManager.instance.modeSelected[2].SetActive(true);
        }
        gameObject.SetActive(false);
        //  LevelSelectionManager.Instance.InitLvls();
    }
    public void BackFromModeSelection()
    {
        player.SetActive(true);
        MenuManager.instance.mainMenupanel.SetActive(true);
        Splash.isBackFromGameplay = false;
        this.gameObject.SetActive(false);
    }

}
