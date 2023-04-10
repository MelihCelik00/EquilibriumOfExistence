using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CoreMusicAudioSource : MonoBehaviour
{
    public Slider _slider;
    private AudioSource _source;
    public float panningValue = 1;
   
    private void Start()
    {
        _slider = FindObjectOfType<Slider>();
        _source = gameObject.GetComponent<AudioSource>();
        AudioController.instance.PlayAudio(AudioNames.AudioName.Core_Game_music3D,false,0f);
    }

    private void Update()
    {
        _source.panStereo = panningValue * _slider.value;
        
    }
}
