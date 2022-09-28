using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float timer;
    public int speed;
    private bool isAnimate;
    public bool isloop;
    Vector3 pos;
    void Start()
    {
        if (!isloop)
            Invoke("StopAnimation", timer);
        pos = target.transform.position;
    }
    void StopAnimation()
    {
        isAnimate = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!isAnimate)
        {
            transform.RotateAround(pos, Vector3.up, speed * Time.deltaTime);

        }

    }
}
