using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanningManager : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;
    private void Start()
    {
        _slider = FindObjectOfType<Slider>();
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        _audioSource.panStereo = 1 * _slider.value;
    }
}
