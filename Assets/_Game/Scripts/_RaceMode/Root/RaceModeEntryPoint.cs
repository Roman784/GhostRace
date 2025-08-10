using Gameplay;
using GameRoot;
using R3;
using System;
using System.Collections;
using UI;
using UnityEngine;
using Utils;

namespace RaceMode
{
    public class RaceModeEntryPoint : SceneEntryPoint
    {
        [Header("Level")]
        [SerializeField] private Level _level;
        [SerializeField] private Car _playerCar;
        [SerializeField] private Car _ghostCar;

        [Header("UI")]
        [SerializeField] private CarTrackingUI _carTrackingUIPrefab;
        [SerializeField] private CountdownUI _countdownUIPrefab;

        public override IEnumerator Run<T>(T enterParams)
        {
            if (enterParams is RaceModeEnterParams gameplayParams)
                yield return Run(gameplayParams);
            else
                throw new ArgumentException($"Failed to convert {typeof(T)} to {typeof(RaceModeEnterParams)}!");
        }

        // Sequential scene initialization.
        private IEnumerator Run(RaceModeEnterParams enterParams)
        {
            var isLoaded = false;

            // Cars.
            _playerCar.LockControl();

            _level.PlacePlayer(_playerCar);
            _level.PlaceGhost(_ghostCar);

            // Countdown at the beginning.
            var timer = new Timer(1f, 2f, 3f);
            var timerSignals = timer.Start();
            timerSignals
                .Where(t => t == 3f)
                .Subscribe(_ => _playerCar.UnlockControl());

            // TEST <-----
            var recorder = new CarMotionRecorder();
            recorder.StartRecording(_playerCar, 0.1f);
            Observable.Timer(TimeSpan.FromSeconds(15)).Subscribe(_ =>
            {
                var records = recorder.StopRecording();
                new CarMotionReplayer().StartReplaying(_ghostCar, records);
            });
            // -----------

            // UI.
            // Car tracking.
            var carTrackingUI = Instantiate(_carTrackingUIPrefab);
            _uiRoot.AttachFullscreenUI(carTrackingUI);
            carTrackingUI.Init();

            carTrackingUI.AddTracker(_playerCar.TrackerPoint, _playerCar.TrackerPrefab);
            carTrackingUI.AddTracker(_ghostCar.TrackerPoint, _ghostCar.TrackerPrefab);

            // Countdown at the beginning.
            var countdownUI = Instantiate(_countdownUIPrefab);
            _uiRoot.AttachFullscreenUI(countdownUI);
            countdownUI.BindView(3, timerSignals);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
