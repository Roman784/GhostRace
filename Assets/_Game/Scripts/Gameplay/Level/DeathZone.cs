using GameRoot;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class DeathZone : MonoBehaviour
    {
        [Inject]
        private SceneProvider _sceneProvider;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                _sceneProvider.TryRestartScene();
            }
        }
    }
}
