using NaveedPkg;
using Unity.RemoteConfig;
using UnityEngine;
using System.Text;

public class GetConfig : MonoBehaviour
{
    public struct userAttributes
    {
    }

    public struct appAttributes
    {
    }

    private string deviceModel;
    // private string deviceName;

    private void Start()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 0)
            return;

        var jo = new AndroidJavaObject("android.os.Build");
        deviceModel = Application.isEditor ? SystemInfo.deviceModel : jo.GetStatic<string>("MODEL");
        ConfigManager.FetchCompleted += SetValues;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>
            (new userAttributes(), new appAttributes());
    }

    private void SetValues(ConfigResponse response)
    {
        var configManager = ConfigManager.appConfig;
      //  Debug.Log("Total Devices " + configManager.GetKeys().Length);
        if (configManager.GetKeys().Length <= 0)
            return;
        for (var i = 0; i < configManager.GetKeys().Length; i++)
        {
            if (!configManager.HasKey("Device" + i))
                continue;
            var blockDevice = configManager.GetString("Device" + i);
            if (!blockDevice.Equals(deviceModel)) continue;
            break;

        }

       // splash.SwitchScene();
    }
}