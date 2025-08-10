using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CarTrackingUI : SceneUI
    {
        private const float TRACKING_SPEED = 14f;

        [SerializeField] private Canvas _canvas;

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
            tracker.transform.SetParent(_canvas.transform, false);

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

                tracker.gameObject.SetActive(IsTargetVisible(target.position));
                tracker.Move(position);
            }
        }

        // Tracker position on the canvas.
        private Vector2 GetTrackerPosition(Vector3 targetPosition)
        {
            var screenPosition = _camera.WorldToScreenPoint(targetPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPosition,
                _canvas.worldCamera,
                out var position
            );

            return position;
        }

        // Is the target in the camera's viewport?
        private bool IsTargetVisible(Vector3 worldPosition)
        {
            var viewportPoint = _camera.WorldToViewportPoint(worldPosition);

            var isInFront = viewportPoint.z > 0;
            var isOnScreen = viewportPoint.x > 0 && viewportPoint.x < 1 &&
                             viewportPoint.y > 0 && viewportPoint.y < 1;

            return isInFront && isOnScreen;
        }
    }
}
