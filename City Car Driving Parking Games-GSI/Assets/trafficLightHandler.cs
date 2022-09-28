using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLightHandler : MonoBehaviour
{
    public GameObject GreenLights;
    public GameObject RedLight;
    public GameObject YellowLight;

    public GameObject BoxCollider;

    public GameObject walkingGirl;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startLighing());

      
    }
    private void Update()
    {
     
    }
    IEnumerator startLighing()
    {
        GreenLights.SetActive(false);
        RedLight.SetActive(true);
        YellowLight.SetActive(false);
        BoxCollider.SetActive(true);
        yield return new WaitForSeconds(2f);
        GreenLights.SetActive(false);
        RedLight.SetActive(false);
        YellowLight.SetActive(true);
        yield return new WaitForSeconds(2f);
        GreenLights.SetActive(true);
        RedLight.SetActive(false);
        YellowLight.SetActive(false);
        BoxCollider.SetActive(false);
        yield return new WaitForSeconds(4f);
        StartCoroutine(startLighing());
    }


}
