using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RaceModeUI : SceneUI
    {
        [SerializeField] private Button _openRecordingModeButton;

        private Subject<Unit> _openRecordingModeSignalSubj;
        public Observable<Unit> OpenRecordingModeSignal => _openRecordingModeSignalSubj;

        private void Awake()
        {
            _openRecordingModeSignalSubj = new Subject<Unit>();
            _openRecordingModeButton.onClick.AddListener(() =>
                _openRecordingModeSignalSubj.OnNext(Unit.Default));
        }
    }
}
