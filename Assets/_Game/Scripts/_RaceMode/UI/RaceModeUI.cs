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
                _openRecordingModeSignalSubj.OnNext(Unit.Default));

            _restartRaceButton.onClick.AddListener(() => RestartScene());
        }
    }
}
