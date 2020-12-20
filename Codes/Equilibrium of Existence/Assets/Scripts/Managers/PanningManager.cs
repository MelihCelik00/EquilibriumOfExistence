using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PanningManager : MonoBehaviour
    {
        private Slider _slider;
        private AudioSource _audioSource;
        private void Start()
        {
            _slider = FindObjectOfType<Slider>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _audioSource.panStereo = 1 * _slider.value;
        }
    }
}
