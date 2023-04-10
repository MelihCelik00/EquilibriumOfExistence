using UnityEngine;

namespace Player
{
    public class PlayAudioByDistance : MonoBehaviour
    {
        private AudioByDistance _audioByDistance;

        private void Awake()
        {
            _audioByDistance = FindObjectOfType<AudioByDistance>();
        }
        
        public void PlayAccordingToDist()
        {
            if (_audioByDistance.isPlayed == false && _audioByDistance._distanceDiff <= _audioByDistance._requiredDistance && _audioByDistance.transform.position.y < _audioByDistance._closestObject.transform.position.y) // If clip is not played, play it :)
            {
                // either shouldn't pass obstacle and distance difference should be shorter than required distance for popup audio
                PlayAudio();
            }

            if (_audioByDistance.isPlayed)
            {
                _audioByDistance._audioSource.volume = (float) (_audioByDistance._requiredDistance / _audioByDistance._distanceDiff); // Manage audio volume by distance, should be increase while approaching
            }
        }

        public void PlayAudio()
        {
            _audioByDistance.isPlayed = true;
            _audioByDistance._audioSource.Play();
            _audioByDistance._audioSource.volume = 0;
        }
        
    }
}