using UnityEngine;
using UnityEngine.Analytics;

namespace NaveedPkg
{
    public class AnalyticsManager : MonoBehaviour
    {
        public static AnalyticsManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SimpleEvent(string str)
        {
            Analytics.CustomEvent(str);
        }

        public void GameplayEvent(string str)
        {
            Analytics.CustomEvent(GameStats.Instance.CurrentLevel + "-" + str);
        }
    }
}