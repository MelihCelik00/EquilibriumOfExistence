using UnityEngine;
using UnityEngine.Audio;

namespace Eoe.Core
{
    public class SoundManager : Singleton<SoundManager>
    {
        private static AudioMixer mainMixerImpl;
        private static AudioMixer MainMixer
        {
            get
            {
                if (mainMixerImpl == null)
                    mainMixerImpl = Resources.Load<AudioMixer>("AudioMixers/Master");

                return mainMixerImpl;
            }
        }

        // Objeden bağımsız sesler için kullanılacak olan ses yöneticisi.

        public AudioSource playerSource;
        public AudioClip
            ButtonBack,
            ButtonSelected,
            ButtonChange,
            FootstepLeft,
            FootstepRight;

        public AudioClip[] Jump;

        private void Start()
        {
            playerSource = transform.Find("Player").GetComponent<AudioSource>();
        }

        public static void SetVolume(string audioGroup, float volumePercentage)
        {
            MainMixer.SetFloat(audioGroup, NormalizeVolume(volumePercentage));
        }

        public static float NormalizeVolume(float rawSlider)
        {
            return Mathf.Log10(rawSlider + Mathf.Epsilon) * 20;
        }


        public static void PlaySound(AudioClip clip, AudioSource source)
        {
            source.PlayOneShot(clip);
        }


        public static void ApplySettings(AudioSettings audioSettings = null)
        {
            if (audioSettings == null)
                audioSettings = Settings.Audio;

            SetVolume("MasterVolume", audioSettings.MasterVolume);
            SetVolume("MusicVolume", audioSettings.MusicVolume);
            SetVolume("SfxVolume", audioSettings.SfxVolume);
            SetVolume("AmbientVolume", audioSettings.AmbientVolume);
        }
    }
}