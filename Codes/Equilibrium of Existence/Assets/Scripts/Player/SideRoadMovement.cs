using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class SideRoadMovement : MonoBehaviour
    {
        [SerializeField] public Slider balanceSlider;

        private PlayerMovement _movement;
        private PlayerDeath _death;
        private PlayerBalance _balance;
        
        public float sideRoad_xPos;
        
        // Straight roads
        public bool mainRoadChecker = true;
        public bool onSideRoad;

        public bool sideRoadChecker;
        
        // new bools
        public bool leftSideRoadChecker;
        public bool rightSideRoadChecker;
        public bool confirmPassToLeft;
        public bool confirmPassToRight;

        public bool inStay;
        public bool giveOkay = true; // Gives confirmation to SetBalance method in PlayerBalance
        public bool canMove = true; // restricts movement after crosswise pass confirmation

        public bool moveStraight;
        
        private void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _death = GetComponentInChildren<PlayerDeath>();
            _balance = GetComponentInChildren<PlayerBalance>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.tag == "SideRoadCorrectionTrigger")
            {
                sideRoadChecker = false;
                leftSideRoadChecker = false;
                rightSideRoadChecker = false;
                
                mainRoadChecker = false;
                onSideRoad = true;
                                
                _movement.controlSideRoad = true;
                
                _movement.NewX = other.gameObject.transform.position.x;
                sideRoad_xPos =  other.gameObject.transform.position.x;
            }
            else if (other.CompareTag("MainRoadCorrectionTrigger"))
            {
                leftSideRoadChecker = false;
                rightSideRoadChecker = false;
                
                mainRoadChecker = true;
                onSideRoad = false;

                _movement.controlSideRoad = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other) // In Progress
        {
            if (other.CompareTag("LeftSideRoadTrig") || other.CompareTag("RightSideRoadTrig") || other.CompareTag("LeftOrStraight") || other.CompareTag("RightOrStraight"))
            {
                inStay = true;
                giveOkay = false;
            }

            if (other.CompareTag("LeftSideRoadTrig"))
            {
                if (!confirmPassToLeft && _balance.balance <= -_balance.maxBalance)
                {
                    Debug.Log("IT WORKSSSSSSSS");
                    confirmPassToLeft = true;
                    confirmPassToRight = false;
                    canMove = false;
                    moveStraight = false;

                }
                else if (_balance.balance >= _balance.maxBalance)
                {

                    _death.ProcessDeath();

                }
            }
            else if (other.CompareTag("RightSideRoadTrig"))
            {
                if (!confirmPassToRight && _balance.balance >= _balance.maxBalance)
                {
                    Debug.Log("IT WORKSSSSSSSS");
                    confirmPassToLeft = false;
                    confirmPassToRight = true;
                    canMove = false;
                    moveStraight = false;
                }
                else if ( _balance.balance <= -_balance.maxBalance)
                {

                    _death.ProcessDeath();

                }
            }
            else if (other.CompareTag("LeftOrStraight"))
            {
                if (!confirmPassToLeft && _balance.balance <= -_balance.maxBalance) // Go left crossroad
                {
                    Debug.Log("IT WORKSSSSSSSS");
                    confirmPassToLeft = true;
                    confirmPassToRight = false;
                    canMove = false;
                    moveStraight = false;
                }
                else if ( _balance.balance >= _balance.maxBalance)
                {

                    _death.ProcessDeath();

                }
            }
            else if (other.CompareTag("RightOrStraight"))
            {
                if (!confirmPassToRight && _balance.balance >= _balance.maxBalance) // Go right crossroad
                {
                    Debug.Log("IT WORKSSSSSSSS");
                    confirmPassToLeft = false;
                    confirmPassToRight = true;
                    canMove = false;
                    moveStraight = false;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // left and ride side road triggers controls the value which comes
            // from stay event and confirms cross movement
            if (other.CompareTag("LeftSideRoadTrig")||other.CompareTag("RightSideRoadTrig"))
            {
                Debug.Log("Left confirmation: " + confirmPassToLeft);
                Debug.Log("Straight Move? " + moveStraight);
                Debug.Log("Curr. Balance: " + _balance.balance);
                Debug.Log("Max Balance: " + _balance.maxBalance);
                
                if (onSideRoad && (!confirmPassToLeft && !confirmPassToRight))
                {
                    moveStraight = true;
                }
            }
            // Turn left or right
            if (other.CompareTag("LeftSideRoadTrig"))
            {                
                inStay = false;
                
                if (confirmPassToLeft )
                {
                    mainRoadChecker = false;
                    leftSideRoadChecker = true;
                    rightSideRoadChecker = false;
                }
                if (onSideRoad && !mainRoadChecker && !confirmPassToLeft )
                {

                    _death.ProcessDeath();

                }

            }
            else if(other.CompareTag("RightSideRoadTrig"))
            {
                inStay = false;
                if (confirmPassToRight)
                {
                    mainRoadChecker = false;
                    rightSideRoadChecker = true;
                    leftSideRoadChecker = false;
                }
                if (onSideRoad && !mainRoadChecker && !confirmPassToRight)
                {

                    _death.ProcessDeath();

                }
            }
            // Straight roads
            else if(other.CompareTag("LeftOrStraight"))
            {
                inStay = false;
                if (confirmPassToLeft)
                {
                    mainRoadChecker = false;
                    rightSideRoadChecker = false;
                    leftSideRoadChecker = true;
                }
                if (onSideRoad && _balance.balance >= _balance.maxBalance)
                {

                    _death.ProcessDeath();

                }
            }
            else if(other.CompareTag("RightOrStraight"))
            {
                inStay = false;
                if (confirmPassToRight)
                {
                    mainRoadChecker = false;
                    rightSideRoadChecker = true;
                    leftSideRoadChecker = false;
                }
                if (onSideRoad && _balance.balance <= -_balance.maxBalance)
                {

                    _death.ProcessDeath();

                }
            }
            else if (other.CompareTag("SideRoadCorrectionTrigger") || other.CompareTag("MainRoadCorrectionTrigger"))
            {
                _balance.balance = 0;
                _movement.xPosition = 0;
                giveOkay = true;
                canMove = true;
                confirmPassToLeft = false;
                confirmPassToRight = false;
            }
        }

        public string ReturnRoadInfo()
        {
            if (leftSideRoadChecker)
            {
                return "left";
            }
            if (rightSideRoadChecker)
            {
                return "right";
            }
            return null;

        }
        
    }
}