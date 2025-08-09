using Zenject;

namespace GameRoot
{
    public class SceneProvider
    { 
        private readonly SceneLoader _sceneLoader;

        private SceneEnterParams _currentSceneParams;

        [Inject]
        public SceneProvider()
        {
            _sceneLoader = new SceneLoader();
        }
    }
}