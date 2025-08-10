using UnityEngine;

namespace Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _playerInitialPoint;
        [SerializeField] private Transform _ghostInitialPoint;

        public void PlacePlayer(Car car)
        {
            PlaceCar(car, _playerInitialPoint);
        }

        public void PlaceGhost(Car car)
        {
            PlaceCar(car, _ghostInitialPoint);
        }

        private void PlaceCar(Car car, Transform point)
        {
            car.transform.position = point.position;
            car.transform.rotation = point.rotation;
        }
    }
}
