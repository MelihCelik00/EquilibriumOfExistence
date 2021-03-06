﻿using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        [NonSerialized] public float xPosition;
        private float yDelta;

        public float Speed // Constructor to get speed of player
        {
            get => yDelta;
            set => yDelta = value;
        }
        
        private Rigidbody2D _playerRb;

        private void Start()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            yDelta = speed;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var position = transform.position;
            _playerRb.MovePosition(new Vector2(xPosition,position.y + yDelta));
        }
    }
}
