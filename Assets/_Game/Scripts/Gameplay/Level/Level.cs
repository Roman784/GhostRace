using UnityEngine;

namespace Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _carInitialPoint;

        public void PlaceCar(Car car)
        {
            car.transform.position = _carInitialPoint.position;
            car.transform.rotation = _carInitialPoint.rotation;
        }
    }
}
