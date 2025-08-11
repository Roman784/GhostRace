using DG.Tweening;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class Car : MonoBehaviour
    {
        [field: SerializeField] public CarTracker TrackerPrefab { get; private set; }
        [field: SerializeField] public Transform TrackerPoint { get; private set; }

        private Rigidbody _rigidbody;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Control lock is better to do via input lock,
        // but I decided not to interfere with SimcadeVehicleController.
        public void LockControl()
        {
            if (_rigidbody != null)
                _rigidbody.isKinematic = true;
        }

        public void UnlockControl()
        {
            if (_rigidbody != null)
                _rigidbody.isKinematic = false;
        }
    }
}
