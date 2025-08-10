using Gameplay;
using GameRoot;
using R3;
using System;
using System.Collections;
using UI;
using UnityEngine;

namespace RaceMode
{
    public class RaceModeEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Level _level;
        [SerializeField] private Car _car;
        [SerializeField] private Car _ghostCar;

        [Header("UI")]
        [SerializeField] private CarTrackingUI _carTrackingUIPrefab;

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

            _level.PlaceCar(_car);
            _level.PlaceCar(_ghostCar);

            // TEST <-----
            var recorder = new CarMotionRecorder();
            recorder.StartRecording(_car, 0.1f);
            Observable.Timer(TimeSpan.FromSeconds(15)).Subscribe(_ =>
            {
                var records = recorder.StopRecording();
                new CarMotionReplayer().StartReplaying(_ghostCar, records);
            });
            // -----------

            // UI.
            // Car tracking.
            var _carTrackingUI = Instantiate(_carTrackingUIPrefab);
            _uiRoot.AttachFullscreenUI(_carTrackingUI);
            _carTrackingUI.Init();

            _carTrackingUI.AddTracker(_car.TrackerPoint, _car.TrackerPrefab);
            _carTrackingUI.AddTracker(_ghostCar.TrackerPoint, _ghostCar.TrackerPrefab);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
