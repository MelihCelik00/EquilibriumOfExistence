using UnityEngine;

namespace CustomCamera
{
    public class CameraLimit : MonoBehaviour
    {
        public static float HeightLimit = -1000;

        private void Awake()
        {
            HeightLimit = transform.position.y;
        }

        private void OnDrawGizmos()
        {
            var offset = new Vector3(15f, 0, 0);
            var position = transform.position;

            Gizmos.color = Color.white;
            Gizmos.DrawLine(position + offset, position - offset);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}