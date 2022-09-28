using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenScript : MonoBehaviour
{
    public TweenPosition position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        position.enabled = true;
    }
}
