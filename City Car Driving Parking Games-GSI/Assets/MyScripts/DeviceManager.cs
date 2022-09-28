using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

namespace NaveedPkg
{
    public class DeviceManager : MonoBehaviour
    {
        public string[] deviceList;

        private bool isInternetAvailable;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("RemoveAds") != 0)
                return;

            //Check For Internet
            StartCoroutine(StopInternetCheckProcess());
            isInternetAvailable = OpenGoogle();

            //Check for Block Devices
            if (deviceList.All(device => SystemInfo.deviceName != device)) return;
           // MyGlobalValues.isAdsBlock = true;
        }

        private IEnumerator StopInternetCheckProcess()
        {
            yield return new WaitForSecondsRealtime(3);
           // MyGlobalValues.isAdsBlock = !isInternetAvailable;
        }

        private bool OpenGoogle()
        {
            var htmlText = GetHtmlFromUri("https://google.com");
            return htmlText != "";
        }

        private string GetHtmlFromUri(string resource)
        {
            var html = string.Empty;
            var req = (HttpWebRequest) WebRequest.Create(resource);
            try
            {
                using (var resp = (HttpWebResponse) req.GetResponse())
                {
                    var isSuccess = (int) resp.StatusCode < 299 && (int) resp.StatusCode >= 200;

                    if (isSuccess)
                    {
                        using (var reader = new StreamReader(resp.GetResponseStream()))
                        {
                            var cs = new char[10];
                            reader.Read(cs, 0, cs.Length);
                            html = cs.Aggregate(html, (current, ch) => current + ch);
                        }
                    }
                }
            }
            catch
            {
                return "";
            }

            return html;
        }
    }
}