using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class AudioByDistance : MonoBehaviour
    {
        public Transform _closestObject;

        public double _distanceDiff;
        public float _requiredDistance;
        public bool isPlayed = false;

        private PlayerMovement _playerS;
        private Vector3 _playerSpeed;
        
        public AudioSource _audioSource;

        private FindClosestObject _findObjects;
        private PlayAudioByDistance _playAudioByDistance;
        
        private void Awake()
        {
            _findObjects = gameObject.AddComponent<FindClosestObject>();
            _playAudioByDistance = gameObject.AddComponent<PlayAudioByDistance>();
            //Debug.Log(_findObjects == null);
        }

        private void Start()
        {
            _playerS = FindObjectOfType<PlayerMovement>();
        }

        private void Update()
        {
            // Method is in a class which is named FindClosestObject
            _closestObject = _findObjects.FindClosestObstacle();  
            
            // Check for if either played once and passed the obstacle
            if (isPlayed  && transform.position.y > _closestObject.transform.position.y)
            {
                isPlayed = false; // if passed and played once, make var false for the next obstacle
                StartCoroutine(WaitAfterAudio());
                _audioSource.volume = 0;
            }

            CheckDistanceAndPlay(_closestObject.transform);
        }

        private void CheckDistanceAndPlay(Transform t_nearest) // get parameter from FindClosestObject method
        {
            // Get audio source component of nearest obstacle
            _audioSource = t_nearest.GetComponent<AudioSource>(); // Solve performance issue later by NOT USING GetComponent
            
            CalculateDistances(t_nearest);
            _playAudioByDistance.PlayAccordingToDist();
        }

        private void CalculateDistances(Transform t_nearest)
        {
            // Basic distance btw two points calculation, prefered to calculate manually
            _distanceDiff = Math.Sqrt(Math.Pow(t_nearest.position.x - this.transform.position.x, 2) +
                                      Math.Pow(t_nearest.position.y - this.transform.position.y, 2));

            // Multiplied speed with 50 because of the usage of FixedUpdate!!!!(FixedUpdate runs 50 times per second)
            _requiredDistance = (float) (_playerS.Speed * 50.0 * _audioSource.clip.length);
        }

        private IEnumerator WaitAfterAudio()
        {
            yield return new WaitForSeconds(0.5f);
        }
        
    }

}