using RaceMode;
using RecordingMode;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class SceneProvider
    {
        private readonly SceneLoader _sceneLoader;

        private SceneEnterParams _currentSceneParams;

        [Inject]
        public SceneProvider(UIRoot ui)
        {
            _sceneLoader = new SceneLoader(ui);
        }

        public void OpenRecordingMode()
        {
            var enterParams = new RecordingModeEnterParams();
            _currentSceneParams = enterParams;

            _sceneLoader.LoadAndRunScene<RecordingModeEntryPoint, RecordingModeEnterParams>
                (Scenes.RECORDING_MODE, enterParams);
        }

        public void OpenRaceMode()
        {
            var enterParams = new RaceModeEnterParams();
            _currentSceneParams = enterParams;

            _sceneLoader.LoadAndRunScene<RaceModeEntryPoint, RaceModeEnterParams>
                (Scenes.RACE_MODE, enterParams);
        }

        public bool TryRestartScene()
        {
            if (_currentSceneParams == null) return false;

            switch (_currentSceneParams.SceneName)
            {
                case Scenes.RACE_MODE:
                    _sceneLoader.LoadAndRunScene<RaceModeEntryPoint, RaceModeEnterParams>
                        (Scenes.RACE_MODE, (RaceModeEnterParams)_currentSceneParams);
                    return true;
                case Scenes.RECORDING_MODE:
                    _sceneLoader.LoadAndRunScene<RecordingModeEntryPoint, RecordingModeEnterParams>
                        (Scenes.RECORDING_MODE, (RecordingModeEnterParams)_currentSceneParams);
                    return true;
            }

            return false;
        }
    }
}