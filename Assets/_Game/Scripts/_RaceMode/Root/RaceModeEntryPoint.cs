using Gameplay;
using GameRoot;
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

            // UI.
            // Car tracking.
            var _carTrackingUI = Instantiate(_carTrackingUIPrefab);
            _uiRoot.AttachFullscreenUI(_carTrackingUI);
            _carTrackingUI.Init();

            _carTrackingUI.AddTracker(_car.TrackerPoint, _car.TrackerPrefab);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
