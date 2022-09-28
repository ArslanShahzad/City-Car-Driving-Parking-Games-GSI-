using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public GameObject[] Arrow;
    int checkpoint_counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCHeckPOintCheck()
    {
        Arrow[checkpoint_counter].SetActive(false);
        checkpoint_counter++;
        if (checkpoint_counter < Arrow.Length)
        {
            Arrow[checkpoint_counter].SetActive(true);
        }
       

    }
}
