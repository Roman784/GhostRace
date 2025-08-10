using DG.Tweening;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class Car : MonoBehaviour
    {
        [field: SerializeField] public CarTracker TrackerPrefab { get; private set; }
        [field: SerializeField] public Transform TrackerPoint { get; private set; }

        private Tween _moveTween;
        private Tween _rotationTween;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        // Twins are not the best option and if there are performance issues,
        // it can be remake to Update.
        public void Move(Vector3 position, float duration = 0f)
        {
            _moveTween?.Kill();
            _moveTween = transform.DOMove(position, duration);
        }

        public void Rotate(Quaternion rotation, float duration = 0f)
        {
            _rotationTween?.Kill();
            _rotationTween = transform.DORotateQuaternion(rotation, duration);
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
            _rotationTween?.Kill();
        }
    }
}
