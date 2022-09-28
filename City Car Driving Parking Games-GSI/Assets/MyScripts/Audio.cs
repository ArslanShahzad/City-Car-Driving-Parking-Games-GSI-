using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace NaveedPkg
{
    public class Audio : MonoBehaviour
    {
        public static Audio Instance;

        public AudioSource clip;
        public AudioSource music;
        public AudioClip menuMusic, gameplayMusic;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            PlayerPrefs.SetFloat("SetSound", .5f);
            PlayerPrefs.SetFloat("SetMusic", .5f);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var pressedBtn = EventSystem.current.currentSelectedGameObject;
            if (pressedBtn == null || pressedBtn.transform.parent.name.Equals("Controller Buttons")) return;
            var btnRootTag = pressedBtn.transform.root.tag;
            if (!btnRootTag.Equals("UICanvas")) return;
            clip.Play();
        }

        private void OnDestroy()
        {
            //RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
        }

        public void SetMusic()
        {
            var activeSceneName = SceneManager.GetActiveScene().name;
            music.clip = activeSceneName.Equals("Gameplay") ? gameplayMusic : menuMusic;
            music.Play();
        }
    }
}