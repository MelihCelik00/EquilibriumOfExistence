using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothness;

    [SerializeField] private Vector3 _offset;

    private Vector2 _cameraPos;

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        _cameraPos = transform.position;
        _cameraPos.y = Mathf.Lerp( transform.position.y,  _target.position.y+_offset.y, Time.deltaTime * _smoothness);
        
        
    }
}
