using UnityEngine;

namespace CustomCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float smoothingSpeed = 12.5f;
        [SerializeField] private Vector3 offset = new Vector3 (0f,0f,-10f);

        private void Start()
        {
            transform.position = player.position + offset;
        }
        private void Update()
        {
            var desiredPosition = player.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothingSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}