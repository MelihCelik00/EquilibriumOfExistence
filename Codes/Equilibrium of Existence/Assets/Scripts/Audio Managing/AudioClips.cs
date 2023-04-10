using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{ 
public class AudioClips : MonoBehaviour
{
    [Header("Music")]
    public AudioClip mainMenuAudioClip;
    public AudioClip coreGameMusicAudioClip;
    [Header("SFX")]
    public AudioClip obstacleAudioClip;
    public AudioClip redLilylAudioClip;
    public AudioClip sideRoadsAudioClip;
    public AudioClip sideRoadsFeedbackAudioClip;
        public AudioClip deathAudioClip;
    
        
    
    public AudioClip GetMainMenuAudioClip()
    {
        return mainMenuAudioClip;
    }

    public AudioClip GetObstaclesAudioClip()
    {
        return obstacleAudioClip; 
    }
}

}
