using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsHandler : MonoBehaviour
{
    public GameObject[] AdsPanels;
    private int _AdsCounter = 0;
    float counterhandler = 3;
    // Start is called before the first frame update
   
    private void Update()
    {
        counterhandler -= Time.unscaledDeltaTime;
        //print("Unscale Time   " + counterhandler);
        if (counterhandler <= 0)
        {


            _AdsCounter++;

           // print(_AdsCounter + " ------- Ads Counter");
            if (_AdsCounter < AdsPanels.Length)
            {


            }
            else
            {
                _AdsCounter = 0;

            }
            for (int i = 0; i < AdsPanels.Length; i++)
            {
                AdsPanels[i].SetActive(false);

            }

            AdsPanels[_AdsCounter].SetActive(true);
            counterhandler = 7;
        }
    }
 
    public void RoadConstructuion()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.airport.road.construction");
    }
    public void WaterTrain()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.train.driving.simulation.game.free");
    }

    public void TrainvsPrado()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.train.prado.racing.game.free");
    }

    public void CarParking()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.prado.car.parking.simulation.free");
    }
    public void OffRoadGSI()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.idealgames.taxi.simulator.game.free");
    }
    public void OffRoadGSS()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.offroad.taxi.driving");
    }
    public void HomeCarParkingGSI()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gsi.home.car.parking.game");
    }
    public void PlaneSimulationGSI()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.idealgames.airplane.flight.game");
    }
    public void ModernParkingGSI()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.idealgames.prado.parking.free.games");
    }
    public void BikeStunt()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gss.tricky.bike.racing");
    }
}
