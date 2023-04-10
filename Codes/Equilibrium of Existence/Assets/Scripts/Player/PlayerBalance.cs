using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Managers;

namespace Player
{
    public class PlayerBalance : MonoBehaviour
    {
        [Header("Public Balance Variables")]
        public float balance;
        public float maxBalance;
        public float minBalance;
       
        
        [Header("Sfield Variables")]
        [SerializeField] private float balanceSensivity;
        [SerializeField] private float maxRange;
        [SerializeField] private float disruptionAmount;
        [SerializeField] private float minRandomTime;
        [SerializeField] private float maxRandomTime;
        [SerializeField] public Slider balanceSlider;
        
        private float _mouseAxis;
        private float _playerOriginX; // 0f
        private float _newX; // new x position for side roads
        private int _dir = 1;
        private PlayerMovement _playerMovement;
        private SideRoadMovement _sideRoadManager;

        //References
        PlayerDeath playerDeath;

        private void Start()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _playerOriginX = transform.position.x;
            StartCoroutine(ChangeDirection());
            _dir = Random.Range(0, 2) == 0 ? 1 : -1;
            playerDeath = GetComponent<PlayerDeath>();
            _sideRoadManager = GetComponentInParent<SideRoadMovement>();
            minBalance = balanceSlider.minValue;
        }

        private void Update()
        {
            if (!PauseMenu.gameIsPaused && !playerDeath.isDead) {
                if (!_playerMovement.inCrosswise && _sideRoadManager.canMove)
                {
                    _playerMovement.xPosition = 0f;
                    //DisruptBalance();
                    //HandleInput();
                    if (_sideRoadManager.mainRoadChecker)
                    {
                        DisruptBalance();
                        HandleInput();
                        AdjustBalance();
                        AdjustPlayerPosition();
                    }
                    else if (_sideRoadManager.onSideRoad)
                    {
                        DisruptBalance();
                        HandleInput();
                        AdjustBalance();
                        AdjustPlayerPosOnSideRoad();
                    }
                }
                else if (!_sideRoadManager.canMove) // Make balance 0 while not moving
                {
                    balance = Mathf.Lerp(balance, 0, 1);
                    _playerMovement.xPosition = Mathf.Lerp(_playerMovement.xPosition, 0, 1);
                }
            }
        }

        private void HandleInput()
        {
            _mouseAxis = Input.GetAxis("Mouse X");
        }

        private void AdjustBalance() 
        {
            if (_mouseAxis != 0f) {
                SetBalance(_mouseAxis * balanceSensivity);
            }
        }
        
        // Two playerPos adjuster will be refactored
        private void AdjustPlayerPosition() 
        {
            var transform1 = transform;
            var playerCoefficient = (maxRange / maxBalance);
            
            if (_sideRoadManager.canMove)
                _playerMovement.xPosition = _playerOriginX + balance * playerCoefficient;
        }

        private void AdjustPlayerPosOnSideRoad()
        {
            var playerCoefficient = (maxRange / maxBalance);
            _newX = _sideRoadManager.sideRoad_xPos;
            
            if (_sideRoadManager.canMove)
                _playerMovement.NewX = _newX + balance * playerCoefficient;
        }

        private void DisruptBalance()
        {
            SetBalance(_dir * disruptionAmount);
        }

        private IEnumerator ChangeDirection()
        {
            _dir = -_dir;
            var rnd = Random.Range(minRandomTime, maxRandomTime);
            yield return new WaitForSeconds(rnd);
            StartCoroutine(ChangeDirection());
        }

        public virtual void SetBalance(float delta)
        {
            balance += delta;
            if ((balance >= maxBalance || balance <= -maxBalance) &&
                !_sideRoadManager.sideRoadChecker && !_sideRoadManager.inStay
                && _sideRoadManager.giveOkay){
                
                // Maximum balance has been reached
                balance = Mathf.Clamp(balance, -maxBalance, maxBalance);
                Debug.Log($"Maximum balance at {balance}");
                playerDeath.ProcessDeath();
                StopCoroutine(playerDeath.deathCoroutine());

            }
            balanceSlider.value = balance / maxBalance;
        }
        
    }
}