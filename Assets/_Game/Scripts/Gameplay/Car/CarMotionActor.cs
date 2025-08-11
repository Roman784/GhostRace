using UnityEngine;

namespace Gameplay
{
    // Designed to be moved via CarMotionReplayer.
    public class CarMotionActor : MonoBehaviour
    {
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private float _positionProgress;
        private float _rotationProgress;

        private float _moveDuration;
        private float _rotateDuration;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private bool _isMoving;
        private bool _isRotating;

        // I decided movements through coroutines or DOTween too expensive.
        private void Update()
        {
            UpdateMove(Time.deltaTime);
            UpdateRotation(Time.deltaTime);
        }

        public void Move(Vector3 position, float duration = 0f)
        {
            if (duration == 0f)
            {
                transform.position = position;
                return;
            }

            _startPosition = transform.position;
            _targetPosition = position;
            _moveDuration = duration;
            _positionProgress = 0f;
            _isMoving = true;
        }

        public void Rotate(Quaternion rotation, float duration = 0f)
        {
            if (duration == 0f)
            {
                transform.rotation = rotation;
                return;
            }

            _startRotation = transform.rotation;
            _targetRotation = rotation;
            _rotateDuration = duration;
            _rotationProgress = 0f;
            _isRotating = true;
        }

        private void UpdateMove(float deltaTime)
        {
            if (!_isMoving) return;

            _positionProgress += deltaTime / _moveDuration;

            if (_positionProgress >= 1f)
            {
                _isMoving = false;
                transform.position = _targetPosition;
                return;
            }

            transform.position = Vector3.Slerp(_startPosition, _targetPosition, _positionProgress);
        }

        private void UpdateRotation(float deltaTime)
        {
            if (!_isRotating) return;

            _rotationProgress += deltaTime / _rotateDuration;

            if (_rotationProgress >= 1f)
            {
                _isRotating = false;
                transform.rotation = _targetRotation;
                return;
            }

            transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, _rotationProgress);
        }
    }
}
