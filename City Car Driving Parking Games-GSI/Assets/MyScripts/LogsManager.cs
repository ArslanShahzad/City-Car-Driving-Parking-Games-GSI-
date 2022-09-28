using UnityEngine;
using Debug = UnityEngine.Debug;

namespace NaveedPkg
{
    public class LogsManager : MonoBehaviour
    {
        public bool enableLogs;

        private void Awake()
        {
            Debug.unityLogger.logEnabled = enableLogs;
        }
    }
}