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
        [SerializeField] private CountdownUI _countdownUIPrefab;

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

            // UI.
            // Countdown at the beginning.
            var countdownUI = Instantiate(_countdownUIPrefab);
            _uiRoot.AttachFullscreenUI(countdownUI);
            countdownUI.BindView(3, timerSignals);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
