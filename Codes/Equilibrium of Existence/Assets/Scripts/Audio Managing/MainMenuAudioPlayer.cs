using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio
{


public class MainMenuAudioPlayer : MonoBehaviour
{
    AudioSource source;
    private AudioClips audioClips;
    GameObject audioSourceSFX;
    [Range(0, 1)] [SerializeField] float volumeScale = 0.7f;

    private void Start()
    {
        audioSourceSFX = GameObject.FindGameObjectWithTag("AudioSourceMainMusic");
        source = audioSourceSFX.GetComponent<AudioSource>();
    }

    //Plays obstacle audio clip for once 
    public void PlayMainMenuAudioClip()
    {
        if (source != null)
        {
            source.PlayOneShot(audioClips.mainMenuAudioClip, volumeScale);
        }
        else Debug.Log("Source not defined in main menu audio player");
    }
}
}