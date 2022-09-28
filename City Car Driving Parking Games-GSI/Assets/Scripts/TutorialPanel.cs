using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{

    void Start()
    {
        if (GameStats.Instance.Tutorial == 1)
        {
            this.gameObject.SetActive(false);
        }
    }

  
}
