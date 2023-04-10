using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    private GameObject[] _obstacles;
    private AudioSource _audioSource;
    private double _distanceDiff;
    private float _requiredDistance;
    private Transform _nearestObject;
    private Rigidbody2D _player;
    private PlayerMovement _playerS;
    private GameObject _closestObject;
    private Vector3 _playerSpeed;

    private bool isPlayed = false;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        _playerS = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        FindClosestObject(); // You can find methods below
        if ((isPlayed== true) && (transform.position.y > _closestObject.transform.position.y)) // Check for if either played once and passed the obstacle
        {
            isPlayed = false; // if passed and played once, make var false for the next obstacle
            StartCoroutine(WaitAfterAudio());
            _audioSource.volume = 0;
        }
        
        CheckAudioAndPlay(_closestObject.transform);
    }

    private void CheckAudioAndPlay(Transform t_nearest) // get parameter from FindClosestObject method
    {
        _audioSource = t_nearest.GetComponent<AudioSource>(); // Get audio source component of nearest obstacle

        _distanceDiff = Math.Sqrt(Math.Pow(t_nearest.position.x - this.transform.position.x, 2) + Math.Pow(t_nearest.position.y - this.transform.position.y, 2)); // Basic distance btw two points calculation, prefered to calculate manually
        _requiredDistance = (float) (_playerS.Speed * 50.0  * _audioSource.clip.length); // Multiplied speed with 50 because of the usage of FixedUpdate!!!!(FixedUpdate runs 50 times per second) 
        
        // X= V*t // DEBUG Starts
        /*Debug.Log("VELOCITY " + _playerS.Speed* 50.0 + "\n" + "Length(t): " + _audioSource.clip.length);
        Debug.Log("Dist diff: " + _distanceDiff);
        Debug.Log("Required dist: " + _requiredDistance);*/
        //DEBUG Ends
        
        
        //ebug.Log("VOLUMEEEEEEEE::::: " + _audioSource.volume); // Volume debugging
        if (isPlayed == false) // If clip is not played, play it :)
        {
            if (_distanceDiff <= _requiredDistance && transform.position.y < _closestObject.transform.position.y)   // either shouldn't pass obstacle and distance difference should be shorter than required distance for popup audio
            {
            isPlayed = true;
            _audioSource.Play();
            _audioSource.volume = 0;

            }
        }

        if (isPlayed)
        {
            _audioSource.volume = (float) (_requiredDistance / _distanceDiff); // Manage audio volume by distance, should be increase while approaching
        }
    }

    private IEnumerator WaitAfterAudio()
    {
        
        yield return new WaitForSeconds(0.5f);
    }

    void FindClosestObject() // Dynamically controls nearest obstacle
    {
        float distanceToClosestObject = Mathf.Infinity;
        _closestObject = null;
        foreach (GameObject _gameObject in _obstacles) // Find GameObjects with the "Obstacle" tag.
        {
            float distanceToObstacle = (_gameObject.transform.position - transform.position).sqrMagnitude;
            if (distanceToObstacle < distanceToClosestObject)
            {
                distanceToClosestObject = distanceToObstacle;
                _closestObject = _gameObject;
                if (transform.position.y>_closestObject.transform.position.y)
                {
                    _closestObject.tag = "PassedObstacle";
                }
            }
        }
    }

    private Vector3 GetPlayerSpeed() // redundant and OLD // DON'T USE PLEASE
    {
        Vector3 oldPosition = transform.position;
        Vector3 speedPerSec = (transform.position-oldPosition) / Time.deltaTime;
        oldPosition = transform.position;
        return speedPerSec;
    }
    
    Transform GetClosestEnemy (Transform[] obstacles) // redundant and OLD // DON'T USE PLEASE
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in obstacles)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}