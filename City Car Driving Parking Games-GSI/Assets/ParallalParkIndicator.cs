using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallalParkIndicator : MonoBehaviour
{
    public bool active;


    public string ID;
    public bool isIncrement = true;
    public bool isDecrement = true;
    public int increament;

    public bool isReverseParking;
    public GameObject ReverseParkingPanel;
    // Start is called before the first frame update
    void Start()
    {
        isIncrement = true;
        isDecrement = true;
        if (isReverseParking)
        {
            ReverseParkingPanel.SetActive(true);
            Invoke("RemoveReverseParking", 3f);
        }
    }
    void RemoveReverseParking()
    {
        ReverseParkingPanel.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
    //    print(other.gameObject.name + "Object Name");

        if (other.gameObject.name == ID)
        {
            active = true;
            isDecrement = true;
            Increament();
        }
       
    }

    void Increament()
    {
        if (isIncrement)
        {
            LevelManager.instance.ParallalParkingHandler(isIncrement);
            isIncrement = false;

        }
    }
   
    void Discreament()
    {
        if (isDecrement)
        {
            LevelManager.instance.ParallalParkingHandler(isDecrement);
            isDecrement = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == ID)
        {
            active = false;
            isIncrement = true;
            Discreament();
        }
    }

}
