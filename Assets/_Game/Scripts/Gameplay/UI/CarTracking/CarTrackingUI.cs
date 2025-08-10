using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CarTrackingUI : SceneUI
    {
        private const float TRACKING_SPEED = 8f;

        [SerializeField] private RectTransform _viewport;

        private HashSet<(CarTracker, Transform)> _trackers = new();
        private Camera _camera;

        private void LateUpdate()
        {
            MoveTrackers(Time.deltaTime);
        }

        public void Init()
        {
            _camera = Camera.main;
        }

        public void AddTracker(Transform target, CarTracker trackerPrefab)
        {
            var tracker = CreateTracker(trackerPrefab);
            tracker.transform.SetParent(_viewport, false);

            tracker.Move(GetTrackerPosition(target.position));

            _trackers.Add((tracker, target));
        }

        private void RemoveTracker(CarTracker tracker, Transform target)
        {
            _trackers.Remove((tracker, target));
            Destroy(tracker.gameObject);
        }

        private CarTracker CreateTracker(CarTracker prefab)
        {
            return Instantiate(prefab);
        }

        private void MoveTrackers(float deltaTime)
        {
            foreach (var (tracker, target) in _trackers)
            {
                // If the target is destroyed at runtime.
                if (target == null)
                {
                    RemoveTracker(tracker, target);
                    break; // The method is invoked every frame, so it is not scary to lose 1 pass.
                           // Otherwise, i can use a linked list.
                }

                var positionOnScreen = GetTrackerPosition(target.position);
                var position = Vector2.Lerp(tracker.Position, positionOnScreen, TRACKING_SPEED * deltaTime);

                tracker.Move(position);
            }
        }

        // Tracker position on the canvas.
        private Vector2 GetTrackerPosition(Vector3 targetPosition)
        {
            Vector2 screenPoint = _camera.WorldToViewportPoint(targetPosition);

            var viewportSize = _viewport.sizeDelta;
            screenPoint.x *= viewportSize.x;
            screenPoint.y *= viewportSize.y;

            screenPoint -= viewportSize * 0.5f;

            return screenPoint;
        }
    }
}
