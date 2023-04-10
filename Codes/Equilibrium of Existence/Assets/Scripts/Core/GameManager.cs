using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR

#endif


namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsPaused;

        public float gravityScale = 3;

        public bool isStartInputPressed;
        [SerializeField] private bool resetStartInput;

        public float linearDrag = 0.5f;

        public delegate void OnGameManagerLoadedHandler();

        public event OnGameManagerLoadedHandler OnGameManagerLoaded;

        public static GameManager I { get; private set; }

        private void Awake()
        {
            if (I == null)
                I = this;
            else
                Destroy(gameObject);

            Settings.Init();

#if UNITY_EDITOR
            LevelManager.SetSceneIndex();
#endif

            LevelEvents.OnLevelStart += () => SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));

            OnGameManagerLoaded?.Invoke();
        }


        private void Start()
        {
            SoundManager.ApplySettings();
            if (resetStartInput && !isStartInputPressed) 
                LevelManager.OnResetLevel += ResetStartInput;
        }

        private void ResetStartInput()
        {
            isStartInputPressed = false;
        }


        public static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}