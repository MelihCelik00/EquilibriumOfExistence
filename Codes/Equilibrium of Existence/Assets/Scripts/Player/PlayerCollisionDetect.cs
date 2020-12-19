using Eoe.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetect : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Obstacle;
    bool isCoroutine = false;
    private Coroutine coroutine;
    

     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (isCoroutine == false)
        {
            LevelManager.ResetLevel();
            Debug.Log("Restarted");
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCoroutine == true)
            {
                StopCoroutine(coroutine);
                Debug.Log("Stopped");
                isCoroutine = false;
                coroutine = StartCoroutine(PlayerCoroutine());
            }
            else
            {
                coroutine = StartCoroutine(PlayerCoroutine());
            }
        }
    }
    IEnumerator PlayerCoroutine()
    {
        Debug.Log("Coroutine start");
        isCoroutine = true;
        yield return new WaitForSeconds(2);
        isCoroutine = false;
        Debug.Log("Couroutine finish");
    }
}
