using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio
{


public class SideRoadsAudioPlayer : MonoBehaviour
{
    AudioSource source;
    private AudioClips audioClips;
    GameObject audioSourceSFX;
    [Range(0, 1)] [SerializeField] float volumeScale = 0.7f;

    private void Start()
    {
        audioSourceSFX = GameObject.FindGameObjectWithTag("AudioSourceSFX");
        source = audioSourceSFX.GetComponent<AudioSource>();
    }

    //Plays obstacle audio clip for once 
    public void PlaySideRoadsAudioClip()
    {
        if (source != null)
        {
            source.PlayOneShot(audioClips.sideRoadsAudioClip, volumeScale);
        }
        else Debug.Log("Source not defined in side roads audio player");
    }
}
}