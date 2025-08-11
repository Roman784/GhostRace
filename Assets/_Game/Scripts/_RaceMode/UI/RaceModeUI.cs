using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RaceModeUI : SceneUI
    {
        [SerializeField] private Button _openRecordingModeButton;
        [SerializeField] private Button _restartRaceButton;

        private Subject<Unit> _openRecordingModeSignalSubj;
        public Observable<Unit> OpenRecordingModeSignal => _openRecordingModeSignalSubj;

        private void Awake()
        {
            _openRecordingModeSignalSubj = new Subject<Unit>();
            _openRecordingModeButton.onClick.AddListener(() =>
                InvokeOpenRecordingModeSignal());

            _restartRaceButton.onClick.AddListener(() => RestartScene());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                InvokeOpenRecordingModeSignal();

            if (Input.GetKeyDown(KeyCode.R))
                RestartScene();
        }

        private void InvokeOpenRecordingModeSignal()
        {
            _openRecordingModeSignalSubj.OnNext(Unit.Default);
            _openRecordingModeSignalSubj.OnCompleted();
        }
    }
}
