using UnityEngine;

namespace Independents
{
    public class RotationEffect : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private float _rotation;

        private void FixedUpdate()
        {
            // Objeyi rotationSpeed hızında kendi etrafında döndür
            _rotation = (_rotation + rotationSpeed) % 360f;
            var q = Quaternion.Euler(0f, 0f, _rotation);
            transform.rotation = q;
        }
    }
}