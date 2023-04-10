using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        //[NonSerialized]
        [SerializeField]public float xPosition;
        private float yDelta;
        private float sideRoadXPos;
        
        public float playerPos;
        
        private SideRoadMovement _sideMovement;
        private NewBalance _newBalance;
        private PlayerBalance _balance;
        
        public bool inCrosswise; // true if player is conveying between roads
        public bool controlSideRoad;
        

        public float Speed // Constructor to get speed of player
        {
            get => yDelta;
            set => yDelta = value;
        }

        public float NewX
        {
            get => sideRoadXPos;
            set => sideRoadXPos = value;
        }

        private Rigidbody2D _playerRb;

        private void Awake()
        {
            _sideMovement = GetComponent<SideRoadMovement>();
        }

        private void Start()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            yDelta = speed;
            _balance = GetComponentInChildren<PlayerBalance>();
        }

        private void FixedUpdate()
        {
            playerPos = transform.position.x;
            //Debug.Log(yDelta); // 0.05
            // conveying to side roads 
            if (_sideMovement.leftSideRoadChecker && !_sideMovement.mainRoadChecker)
            {
                MoveCrosswise();
            }
            else if (_sideMovement.rightSideRoadChecker && !_sideMovement.mainRoadChecker)
            {
                Debug.Log("In Crosswise");
                MoveCrosswise();   
            }
            // movement on main road
            else if (_sideMovement.mainRoadChecker && !_sideMovement.onSideRoad)
            {
                Debug.Log("In Straight Move");
                Move(); 
            }
            // movement on side road
            else if (_sideMovement.onSideRoad )
            {
                MoveOnSideRoad();
            }
            
            // Go left or right side roads
            else if (_sideMovement.sideRoadChecker && !_sideMovement.mainRoadChecker && !_sideMovement.onSideRoad)
            {
                var position = transform.position;
                if (gameObject.transform.position.x < 0)
                {
                    GoRightSideRoad(position);
                }
                else if (gameObject.transform.position.x > 0)
                {
                    GoLeftSideRoad(position);
                }
            }
            
        }

        public void Move() // This method is for make player walk on the main road
        {
            if (inCrosswise)
            {
                yDelta *= 1.5f;
                inCrosswise = false;
            }   
            
            var position = transform.position;
            _playerRb.MovePosition(new Vector2(xPosition,position.y + yDelta));
        }

        public void MoveOnSideRoad() 
        {
            if (inCrosswise)
            {
                yDelta *= 1.5f;
                inCrosswise = false;
            }   
            var position = transform.position;
            
            if (controlSideRoad)
            {
                _playerRb.MovePosition(new Vector2(sideRoadXPos,position.y + yDelta));
                controlSideRoad = false;
            }
            _playerRb.MovePosition(new Vector2(sideRoadXPos,position.y + yDelta));
            
        }

        public void MoveCrosswise()
        {
            if (!inCrosswise)
            {
                yDelta /= 1.5f;
                inCrosswise = true;
            }
            _sideMovement.mainRoadChecker = false;
            var position = transform.position;
            if (_sideMovement.balanceSlider.value < 0)
            {
                GoLeftSideRoad(position);
            }
            
            else if (_sideMovement.balanceSlider.value > 0)
            {
                GoRightSideRoad(position);
            }
        }

        public void GoLeftSideRoad(Vector3 position)
        {
            _playerRb.MovePosition(new Vector2((position.x - yDelta), position.y + yDelta));
        }

        public void GoRightSideRoad(Vector3 position)
        {
            _playerRb.MovePosition(new Vector2((position.x + yDelta), position.y + yDelta));
        }
        
    }
    
    public class NewBalance : PlayerBalance
    {
        private PlayerBalance pBalance;

        public PlayerBalance PBalance
        {
            get => pBalance;
            set => pBalance = value;
        }
        
        public override void SetBalance(float delta)
        {
            pBalance.maxBalance = 500f;
            balance += delta;
            if (balance >= maxBalance || balance <= -maxBalance) {
                
                // Maximum balance has been reached
                balance = Mathf.Clamp(balance, -maxBalance, maxBalance);
                Debug.Log($"Maximum balance at {balance}");
                //playerDeath.ProcessDeath();

            }
            PBalance.balanceSlider.value = balance / maxBalance;
        }
    }
    
}
