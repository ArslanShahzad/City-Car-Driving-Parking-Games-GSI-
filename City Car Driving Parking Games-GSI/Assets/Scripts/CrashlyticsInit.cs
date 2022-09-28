using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;

public class CrashlyticsInit : MonoBehaviour
{
    public static CrashlyticsInit _Instance;
    public bool FireBaseInit = false;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }
    void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
        }
    }
    
}
