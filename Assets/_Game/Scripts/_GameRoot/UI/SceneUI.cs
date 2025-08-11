using GameRoot;
using Zenject;

namespace UI
{
    public class SceneUI : FullscreenUI
    {
        protected SceneProvider _sceneProvider;

        [Inject]
        private void Construct(SceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }

        protected void RestartScene()
        {
            _sceneProvider.TryRestartScene();
        }
    }
}