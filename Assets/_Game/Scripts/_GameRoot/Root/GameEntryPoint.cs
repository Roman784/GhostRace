using System.Collections;
using UnityEngine;
using Utils;

namespace GameRoot
{
    public sealed class GameEntryPoint : SceneEntryPoint
    {
        private void Start()
        {
            var enterParams = new SceneEnterParams(Scenes.BOOT);
            Coroutines.Start(Run(enterParams));
        }

        // Application initializing: loading data and setting up.
        public override IEnumerator Run<T>(T _)
        {
            SetAppSettings();

            yield return null;

            StartGame();
        }

        private void SetAppSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // Starts the first scene the player will see.
        private void StartGame()
        {
            _sceneProvider.OpenRecordingMode();
        }
    }
}