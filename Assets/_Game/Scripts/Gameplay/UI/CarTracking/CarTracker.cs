using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class CarTracker : MonoBehaviour
    {
        private RectTransform _transform;

        public Vector2 Position => _transform.anchoredPosition;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void Move(Vector2 position)
        {
            _transform.anchoredPosition = position;
        }
    }
}
