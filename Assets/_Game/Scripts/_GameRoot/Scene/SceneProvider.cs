using RaceMode;
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

        public void OpenRaceMode()
        {
            var enterParams = new RaceModeEnterParams();
            _currentSceneParams = enterParams;

            _sceneLoader.LoadAndRunScene<RaceModeEntryPoint, RaceModeEnterParams>
                (Scenes.RACE_MODE, enterParams);
        }
    }
}