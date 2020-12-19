using UnityEngine;

namespace Core
{
    public class GameplaySettings
    {
        public bool IsDebugMode = true;
        public bool IsMiddleMouseReset = true;
        public bool IsStartInput = false;
    }

    public class GraphicsSettings
    {
        public bool IsPostProcessing = true;
        public bool IsBloom = true;
    }

    public class AudioSettings
    {
        private float _masterVolume = 5;
        private float _musicVolume = 5;
        private float _sfxVolume = 5;
        private float _ambientVolume = 5;

        public bool IsMasterEnabled = true;
        public bool IsMusicEnabled = true;
        public bool IsSfxEnabled = true;
        public bool IsAmbientEnabled = true;

        public float MasterVolume
        {
            get => IsMasterEnabled ? _masterVolume : Mathf.Epsilon;
            set => _masterVolume = value;
        }
        public float MusicVolume
        {
            get => IsMusicEnabled ? _musicVolume : Mathf.Epsilon;
            set => _musicVolume = value;
        }
        public float SfxVolume
        {
            get => IsSfxEnabled ? _sfxVolume : Mathf.Epsilon;
            set => _sfxVolume = value;
        }
        public float AmbientVolume
        {
            get => IsAmbientEnabled ? _ambientVolume : Mathf.Epsilon;
            set => _ambientVolume = value;
        }
    }

    public static class Settings
    {
        public static GameplaySettings Gameplay;
        public static GraphicsSettings Graphics;
        public static AudioSettings Audio;

        public static void Init()
        {
            // TODO: Create new if previous save is not present

            Gameplay = new GameplaySettings();
            Graphics = new GraphicsSettings();
            Audio = new AudioSettings();
        }
    }
}