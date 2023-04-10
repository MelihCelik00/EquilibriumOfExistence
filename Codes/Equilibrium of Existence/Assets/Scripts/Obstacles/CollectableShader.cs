using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableShader : MonoBehaviour
{
    public Material objectD;
    public float fade = 1;
    public float integer = 0;
    
    void Start()
    {
        fade = 1;
        integer = 0;
        objectD.SetFloat(("_Fade"), fade);
        objectD.SetFloat(("_Integer"), integer);
    }
    void Update()
    {
        objectD.SetFloat(("_Fade"), fade);
        objectD.SetFloat(("_Integer"), integer);

        
        if (Input.GetKey(KeyCode.K))
        {

            fade = 1;
            integer = -0.5f;

        }
        if (Input.GetKey(KeyCode.L))
        {
            fade = 0;
            integer = 0;
        }

    }
    
}
