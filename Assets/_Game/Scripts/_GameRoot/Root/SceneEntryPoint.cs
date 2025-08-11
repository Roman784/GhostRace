using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected UIRoot _uiRoot;
        protected SceneProvider _sceneProvider;
        protected SceneUIFactory _sceneUIFactory;

        [Inject]
        private void Construct(UIRoot uiRoot, SceneProvider sceneProvider,
                               SceneUIFactory sceneUIFactory)
        {
            _uiRoot = uiRoot;
            _sceneProvider = sceneProvider;
            _sceneUIFactory = sceneUIFactory;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}