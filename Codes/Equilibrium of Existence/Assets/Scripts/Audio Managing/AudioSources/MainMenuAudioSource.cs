using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioSource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioController.instance.PlayAudio(AudioNames.AudioName.Main_Menu_Music,false,0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
