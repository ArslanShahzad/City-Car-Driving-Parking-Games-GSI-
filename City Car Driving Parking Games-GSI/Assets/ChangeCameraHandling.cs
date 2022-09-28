using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraHandling : MonoBehaviour
{
    public garageOrbit manager;

    public float minY;
    public float maxY;
    public float DistanceMin;
    public float DistanceMax;

    public bool isValueChange = false;
    // Start is called before the first frame update
    void Start()
    {
        minY = manager.yMinLimit;
        maxY = manager.yMaxLimit;
        DistanceMin = manager.distanceMin;
        DistanceMax = manager.distanceMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCameraChange()
    {
        isValueChange = !isValueChange;
        if (isValueChange)
        {
            manager.yMinLimit = 70f;
            manager.yMaxLimit = 70f;
            manager.distanceMin = 30f;
            manager.distanceMax = 30f;
        }
        else
        {
            manager.yMinLimit = minY;
            manager.yMaxLimit = maxY;
            manager.distanceMin = DistanceMin;
            manager.distanceMax = DistanceMax;
        }
    }
}
