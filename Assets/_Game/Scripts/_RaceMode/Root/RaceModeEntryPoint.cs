using DG.Tweening;
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
        [SerializeField] private RaceModeUI _uiPrefab;
        [SerializeField] private CarTrackingUI _carTrackingUIPrefab;
        [SerializeField] private CountdownUI _countdownUIPrefab;

        private readonly CompositeDisposable _disposables = new();
        private Timer _countdownTimer;
        private CarMotionReplayer _replayer;

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

            // Ghost motion replaying.
            _replayer = new CarMotionReplayer();

            // Countdown at the beginning and start of the race.
            _countdownTimer = new Timer(1f, 2f, 3f);
            var timerSignals = _countdownTimer.Start();
            timerSignals
                .Where(t => t == 3f)
                .Subscribe(_ =>
                {
                    _playerCar.UnlockControl();

                    var ghostCarMotionActor = _ghostCar.GetComponent<CarMotionActor>();
                    if (ghostCarMotionActor == null)
                        throw new NullReferenceException($"Failed to get {typeof(CarMotionActor)} from ghost car!");

                    _replayer.StartReplaying(ghostCarMotionActor, enterParams.Records);
                })
                .AddTo(_disposables);

            // UI.
            // Main ui on scene.
            var ui = _sceneUIFactory.Create(_uiPrefab);

            // Open recording mode.
            ui.OpenRecordingModeSignal
                .Subscribe(_ => _sceneProvider.OpenRecordingMode())
                .AddTo(_disposables);

            // Car tracking.
            var carTrackingUI = _sceneUIFactory.Create(_carTrackingUIPrefab);
            carTrackingUI.Init();

            carTrackingUI.AddTracker(_playerCar.TrackerPoint, _playerCar.TrackerPrefab);
            carTrackingUI.AddTracker(_ghostCar.TrackerPoint, _ghostCar.TrackerPrefab);

            // Countdown at the beginning.
            var countdownUI = _sceneUIFactory.Create(_countdownUIPrefab);
            countdownUI.BindView(3, timerSignals);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            _countdownTimer?.Stop();
            _replayer?.StopReplaying();

            DOTween.CompleteAll();
            DOTween.KillAll();
        }
    }
}
