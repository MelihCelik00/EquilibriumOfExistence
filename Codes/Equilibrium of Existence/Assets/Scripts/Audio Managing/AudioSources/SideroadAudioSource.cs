using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class SideroadAudioSource : MonoBehaviour
{
    public GameObject player;
    string roadInfo;
    SideRoadMovement sideroad;
    
    // Start is called before the first frame update
    void Start()
    {
        sideroad = FindObjectOfType<SideRoadMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        roadInfo = sideroad.ReturnRoadInfo();
        if (roadInfo != null)
        {
            if(roadInfo == "right")
            {
                gameObject.GetComponent<AudioSource>().panStereo = 1;
                AudioController.instance.PlayAudio(AudioNames.AudioName.Side_Road_SFX3D, false, 0);
            }
            if (roadInfo == "left")
            {
                gameObject.GetComponent<AudioSource>().panStereo = -1;
                AudioController.instance.PlayAudio(AudioNames.AudioName.Side_Road_SFX3D, false, 0);
            }
        }
    }
}
