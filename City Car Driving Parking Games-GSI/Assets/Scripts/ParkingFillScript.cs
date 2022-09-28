using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingFillScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LevelCompleteCall()
    {
        if (LevelManager.instance)
        {
            LevelManager.instance.LevelComplteCallBack();
        }
    }
}
