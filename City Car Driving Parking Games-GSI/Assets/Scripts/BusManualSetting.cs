using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// wajid add scripts
/// </summary>
[System.Serializable]
public class Gear
{
    public int gearNo;
    public int speed;
    public int gearTorque;

}
[System.Serializable]
public class Buse
{
    public int DefaultBrakeForce;
    public Gear[] Gears;
    public float TurnForce;
    public float BackToNormalTurnForce;
    public int UpgradedMaxSpeed;
}
public class BusManualSetting : MonoBehaviour
{
    public Buse[] Buses;
    public static BusManualSetting Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Buse GetCurrentSetting(int busNo)
    {

        ///////// Check Motor Torque UpgradeSetting of Bus
        //int SpeedUpgradelevel = PlayerPrefs.GetInt("Speed" + busNo);
        //int TorqueUpgradedLevel = PlayerPrefs.GetInt("TorqueLevel" + busNo);
        //int BrakeForceUpgradelevel = PlayerPrefs.GetInt("Brake" + busNo);
        //int HandlingUpgradelevel = PlayerPrefs.GetInt("Handling" + busNo);
        //Buses[busNo].UpgradedMaxSpeed = (SpeedUpgradelevel * 5);
        //Adding Upgrade torque to Default Torque of Buses
        //foreach (Gears gear in Buses[0].Gears)
        //{
        //    gear.gearTorque += (TorqueUpgradedLevel * 30);
        //    gear.speed += (TorqueUpgradedLevel * 5);
        //}
        //for (int i = 0; i < Buses[0].Gears.Length; i++)
        //{

        //    if (i != 0)
        //        Buses[0].Gears[i].speed += (SpeedUpgradelevel * 5);

        //}
        Buses[0].TurnForce += (1 * 0.8f);
        Buses[0].BackToNormalTurnForce += (1 * 0.8f);
        //Buses[0].DefaultBrakeForce += (BrakeForceUpgradelevel * 100);
        return Buses[0];

    }

}

