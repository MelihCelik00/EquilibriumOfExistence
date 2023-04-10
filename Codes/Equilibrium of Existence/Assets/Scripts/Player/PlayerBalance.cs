using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerBalance : MonoBehaviour
    {
        [Header("Public Balance Variables")]
        public float balance;
        public float maxBalance;
       
        
        [Header("Sfield Variables")]
        [SerializeField] private float balanceSensivity;
        [SerializeField] private float maxRange;
        [SerializeField] private float disruptionAmount;
        [SerializeField] private float minRandomTime;
        [SerializeField] private float maxRandomTime;
        [SerializeField] private Slider balanceSlider;
        
        private float _mouseAxis;
        private float _playerOriginX;
        private int _dir = 1;
        private PlayerMovement _playerMovement;

        //References
         PlayerDeath playerDeath;
       

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerOriginX = transform.position.x;
            StartCoroutine(ChangeDirection());
            _dir = Random.Range(0, 2) == 0 ? 1 : -1;
            playerDeath = GetComponent<PlayerDeath>();
        }

        private void Update()
        {
            _playerMovement.xPosition = 0f;
            DisruptBalance();
            HandleInput();
            AdjustBalance();
            AdjustPlayerPosition();
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

        private void AdjustPlayerPosition()
        {
            var transform1 = transform;
            var pos = transform1.position;
            var playerCoefficient = (maxRange / maxBalance);
            _playerMovement.xPosition = _playerOriginX + balance * playerCoefficient;
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

        private void SetBalance(float delta)
        {
            balance += delta;
            if (balance >= maxBalance || balance <= -maxBalance) {
                
                // Maximum balance has been reached
                balance = Mathf.Clamp(balance, -maxBalance, maxBalance);
                Debug.Log($"Maximum balance at {balance}");
                LevelManager.ResetLevel();
                //playerDeath.ProcessDeath();

            }
            balanceSlider.value = balance / maxBalance;
        }
    }
}