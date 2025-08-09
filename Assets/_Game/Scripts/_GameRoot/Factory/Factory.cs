using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameRoot
{
    public abstract class Factory
    {
        protected DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        // Loading can be done through a special asset provider.
        // For example ResourcesAssetProvider : IAssetProvider.
        public T LoadPrefab<T>(string path) where T : Object
        {
            T prefab = Resources.Load<T>(path);

            if (prefab == null)
                throw new NullReferenceException($"Failed to load prefab '{path}'!");

            return prefab;
        }
    }
}