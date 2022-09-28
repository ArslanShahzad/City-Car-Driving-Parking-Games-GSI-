using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public LevelHandler levelHandler;
    public GameObject[] Locks;
    private void Start()
    {
        for(int i = 1; i <= levelHandler.LevelsUnlocked; i++)
        {
            Locks[i-1].SetActive(false);
        }
    }
    public void OnLevelSelect(int levelNumber)
    {
        if(levelNumber<=levelHandler.LevelsUnlocked)
            levelHandler.CurrentLevel = levelNumber;
    }

    //method to call when any level completes
    public void unlockLevel()
    {
        if (levelHandler.CurrentLevel <= levelHandler.LevelsUnlocked)
        {
            levelHandler.LevelsUnlocked += 1;
        }
    }
}
