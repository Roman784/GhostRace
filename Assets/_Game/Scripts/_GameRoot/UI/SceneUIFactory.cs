using Zenject;

namespace UI
{
    public class SceneUIFactory
    {
        private readonly DiContainer _container;
        private readonly UIRoot _uiRoot;

        [Inject]
        private SceneUIFactory(DiContainer container, UIRoot uiRoot)
        {
            _container = container;
            _uiRoot = uiRoot;
        }

        // Creates and attaches a UI to the root.
        public T Create<T>(T prefab) where T : SceneUI
        {
            var newUI = _container.InstantiatePrefabForComponent<T>(prefab);
            _uiRoot.AttachFullscreenUI(newUI);

            return newUI;
        }
    }
}
