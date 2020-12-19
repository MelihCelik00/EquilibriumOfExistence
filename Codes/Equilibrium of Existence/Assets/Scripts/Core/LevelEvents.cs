using UnityEngine;

namespace Core
{
    public class LevelEvents : MonoBehaviour
    {
        public delegate void OnLevelAwakeHandler();

        public static event OnLevelAwakeHandler OnLevelAwake;

        public delegate void OnLevelStartHandler();

        public static event OnLevelStartHandler OnLevelStart;

        private void Awake()
        {
            OnLevelAwake?.Invoke();
        }

        private void Start()
        {
            OnLevelStart?.Invoke();
        }
    }
}