using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float balanceSensivity;
        [SerializeField] private float maxRange;
        [SerializeField] private float maxBalance;
        [SerializeField] private Slider balanceSlider;
        [SerializeField] private float disruptionAmount;
        [SerializeField] private float minRandomTime;
        [SerializeField] private float maxRandomTime;
        
        private float _balance;
        private float _mouseAxis;
        private float _playerOriginX;
        private int _dir = 1;

        private void Start()
        {
            _playerOriginX = transform.position.x;
            StartCoroutine(ChangeDirection());
        }

        private void Update()
        {
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
            transform1.position = new Vector3(_playerOriginX + _balance * playerCoefficient, pos.y, pos.z);
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
            _balance += delta;
            if (_balance >= maxBalance || _balance <= -maxBalance) {
                // Maximum balance has been reached
            }

            _balance = Mathf.Clamp(_balance, -maxBalance, maxBalance);
            balanceSlider.value = _balance / maxBalance;
        }
    }
}