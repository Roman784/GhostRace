using UI;
using UnityEngine;

namespace Gameplay
{
    public class Car : MonoBehaviour
    {
        [field: SerializeField] public CarTracker TrackerPrefab { get; private set; }
        [field: SerializeField] public Transform TrackerPoint { get; private set; }
    }
}
