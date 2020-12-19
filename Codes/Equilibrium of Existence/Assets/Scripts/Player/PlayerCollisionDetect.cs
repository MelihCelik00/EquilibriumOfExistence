using Eoe.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetect : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Obstacle;

     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        LevelManager.ResetLevel();
    }
    
        

}
