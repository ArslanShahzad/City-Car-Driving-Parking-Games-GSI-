using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Naveed.Extensions;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelSelectionManager : MonoBehaviour
{
    public static LevelSelectionManager Instance;
    public int totalLvls;
    public GameObject lvlPrefab;
    public RectTransform lvlContent;
    public Text totalLvlsUnlock, lvlDetailText;
    public Sprite[] typeSprite;
    public GameObject player;
    //public LevelObjectivesArray levelObjectivesArray;
    private GridLayoutGroup contentLayoutGroup;
    private void Awake()
    {
        Instance = this;
        var totalLvlsUnlocked = GameStats.Instance.UnlockLevel + 1;
        totalLvlsUnlock.text = "LEVEL " + (totalLvlsUnlocked <= 9 ? ("0" + totalLvlsUnlocked) : ("" + totalLvlsUnlocked));
    }

    private void Start()
    {
        InitLvls();
    }
    int val;
    public void InitLvls()
    {
        contentLayoutGroup = lvlContent.GetComponent<GridLayoutGroup>();
        Vector2 size = lvlContent.sizeDelta;
        lvlContent.sizeDelta = size;
        for (int i = 0; i < totalLvls; i++)
        {
            var lvlObj = Instantiate(lvlPrefab, lvlContent);
            if (SaveValues.isClassicMode)
            {
                val = i;
                lvlObj.name = (i <= 9 ? "0" : "") + i;
            }
            if (SaveValues.isModernMode)
            {
                val = i + 40;
                lvlObj.name = val.ToString();
            }
            if (SaveValues.isChallengeMode)
            {
                val = i + 80;
                lvlObj.name = val.ToString();
            }
            lvlObj.transform.Find("LvlNo").GetComponent<Text>().text = (val < 9 ? "0" : "") + (val + 1);
            lvlObj.transform.Find("LvlNo").gameObject.SetActive(false);
            var statusObj = lvlObj.transform.FindChildRecursive("StatusIcon");
            if (val == 40 || val == 0 || val == 80)
            {
                statusObj.GetComponent<Image>().sprite = typeSprite[0];
                lvlObj.transform.Find("LvlNo").gameObject.SetActive(true);
            }
            //if (val == 80)
            //{
            //    statusObj.GetComponent<Image>().sprite = typeSprite[0];
            //    lvlObj.transform.Find("LvlNo").gameObject.SetActive(true);
            //}
            for (int j = 0; j < SaveValues.instance.unlockLvl.Count; j++)
            {
                if (val == SaveValues.instance.unlockLvl[j])
                {
                    statusObj.GetComponent<Image>().sprite = typeSprite[0];
                    lvlObj.transform.Find("LvlNo").gameObject.SetActive(true);
                    var tempColor = statusObj.GetComponent<Image>().color;
           
                    tempColor.a = 1f;
                    statusObj.GetComponent<Image>().color = tempColor;
                }
            }
            for (int k = 0; k < SaveValues.instance.FailedLvl.Count; k++)
            {
                if (val == SaveValues.instance.FailedLvl[k])
                {
                    if (val != 0 && val != 20 && val != 40)
                    {
                        var tempColor = statusObj.GetComponent<Image>().color;
                        lvlObj.transform.Find("LvlNo").gameObject.SetActive(true);
                        statusObj.GetComponent<Image>().sprite = typeSprite[3];
                        statusObj.GetComponent<Image>().color = Color.red;
                    }
                }
            }
        }
    }
    public void SelectLvl(int lvl)
    {

        //    AdScript.adScript.ShowInterstitial();
        AdScript.adScript.RemoveBanner();
        if (SaveValues.instance.FailedLvl.Contains(lvl) || SaveValues.instance.unlockLvl.Contains(lvl) || lvl == 40 || lvl == 80|| GameStats.Instance.Tutorial == 0 || lvl == 0)
       {
            GameStats.Instance.CurrentLevel = lvl;
            SoundManager.Instance.ButtonClickSound();
            MenuManager.instance.loading.SetActive(true);
            print("OnLevel Stat----------------------------------");
           
            SceneManager.LoadSceneAsync("GamePlay");
            this.gameObject.SetActive(false);
        }


    }
    public void Back(GameObject obj)
    {
        obj.SetActive(false);
        MenuManager.instance.modeSelectionPanel.SetActive(true);
        Splash.isBackFromGameplay = false;
    }
}