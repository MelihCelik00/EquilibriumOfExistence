using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector3 _offSet;

    private void Start()
    {
        transform.position = _offSet;
    }

    private void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.up * ( speed * Time.deltaTime));
    }

}
