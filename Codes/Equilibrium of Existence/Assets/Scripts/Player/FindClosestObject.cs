using UnityEngine;

namespace Player
{
    public class FindClosestObject : MonoBehaviour, IFindClosestObject
    {
        private GameObject[] _obstacles;
        private Transform _nearestObject;
        private GameObject _closestObject;
    
        private void Start()
        {
            _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        }

        public Transform FindClosestObstacle() // Dynamically controls nearest obstacle
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

            return _closestObject.transform;
        }
    
    }
}
