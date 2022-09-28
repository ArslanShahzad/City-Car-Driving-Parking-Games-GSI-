using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NaveedPkg
{
    public class Splash : MonoBehaviour
    {
        public GameObject userReportingPanel;
        public GameObject pkgManagerObj;

        private void Awake()
        {
            //if (PlayerPrefs.GetInt("RemoveAds") != 0)
            //    MyGlobalValues.isAdsBlock = true;

            PlayerPrefs.SetFloat("SetSound", 1);
            PlayerPrefs.SetFloat("SetMusic", 1);
        }

        private void Start()
        {
            StartCoroutine(SwitchScene("MyAds"));
        }

        private IEnumerator SwitchScene(string sceneName)
        {
            yield return new WaitForSeconds(2f);
            userReportingPanel.SetActive(true);
            pkgManagerObj.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneName);
        }
    }
}