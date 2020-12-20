using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionDetect : MonoBehaviour
    {
        private bool _isJump;
        private Animator _anim;
        private static readonly int TrigJump = Animator.StringToHash("trigJump");

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isJump)
            {
                LevelManager.ResetLevel();
                Debug.Log("Death");
            }
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isJump) {
                    Jump();
                }
            }
        }
        private void Jump()
        {
            _isJump = true;
            _anim.SetTrigger(TrigJump);
        }

        private void Ground()
        {
            // Animation event
            _isJump = false;
        }
    }
}