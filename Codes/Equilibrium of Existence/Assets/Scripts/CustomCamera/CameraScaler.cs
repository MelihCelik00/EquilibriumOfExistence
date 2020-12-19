﻿using UnityEngine;

namespace Eoe.CustomCamera
{
    public class CameraScaler : MonoBehaviour
    {
        // Set this to your target aspect ratio, eg. (16, 9) or (4, 3).
        [SerializeField] private Vector2 targetAspect = new Vector2(16, 9);
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            UpdateCrop();
        }


        // Call this method if your window size or target aspect change.
        private void UpdateCrop()
        {
            // Determine ratios of screen/window & target, respectively.
            var screenRatio = Screen.width / (float) Screen.height;
            var targetRatio = targetAspect.x / targetAspect.y;

            if (Mathf.Approximately(screenRatio, targetRatio))
            {
                // Screen or window is the target aspect ratio: use the whole area.
                _camera.rect = new Rect(0, 0, 1, 1);
            }
            else if (screenRatio > targetRatio)
            {
                // Screen or window is wider than the target: pillarbox.
                var normalizedWidth = targetRatio / screenRatio;
                var barThickness = (1f - normalizedWidth) / 2f;
                _camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
            }
            else
            {
                // Screen or window is narrower than the target: letterbox.
                var normalizedHeight = screenRatio / targetRatio;
                var barThickness = (1f - normalizedHeight) / 2f;
                _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
            }
        }
    }
}