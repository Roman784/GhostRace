using Gameplay;
using GameRoot;
using R3;
using System;
using System.Collections;
using UI;
using UnityEngine;
using Utils;

namespace RecordingMode
{
    public class RecordingModeEntryPoint : SceneEntryPoint
    {
        [Header("Level")]
        [SerializeField] private Level _level;
        [SerializeField] private Car _playerCar;

        [Header("UI")]
        [SerializeField] private RecordingModeUI _uiPrefab;
        [SerializeField] private CountdownUI _countdownUIPrefab;

        private CarMotionRecorder _recorder;

        public override IEnumerator Run<T>(T enterParams)
        {
            if (enterParams is RecordingModeEnterParams gameplayParams)
                yield return Run(gameplayParams);
            else
                throw new ArgumentException($"Failed to convert {typeof(T)} to {typeof(RecordingModeEnterParams)}!");
        }

        // Sequential scene initialization.
        private IEnumerator Run(RecordingModeEnterParams enterParams)
        {
            var isLoaded = false;

            // Cars.
            _playerCar.LockControl();
            _level.PlaceGhost(_playerCar); // Position of the ghost, as its path is currently being recorded.

            // Countdown at the beginning.
            var timer = new Timer(1f, 2f, 3f);
            var timerSignals = timer.Start();
            timerSignals
                .Where(t => t == 3f)
                .Subscribe(_ => _playerCar.UnlockControl());

            // Motion recording.
            _recorder = new CarMotionRecorder();
            _recorder.StartRecording(_playerCar, 0.1f);

            // UI.
            var ui = Instantiate(_uiPrefab);
            _uiRoot.AttachFullscreenUI(ui);

            // Stop recording.
            ui.StopRecordingSignal.Subscribe(_ =>
            {
                var records = _recorder.StopRecording();
                _sceneProvider.OpenRaceMode(records);
            });

            // Countdown at the beginning.
            var countdownUI = Instantiate(_countdownUIPrefab);
            _uiRoot.AttachFullscreenUI(countdownUI);
            countdownUI.BindView(3, timerSignals);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }

        private void OnDestroy()
        {
            _recorder?.StopRecording();
        }
    }
}
