using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecordingModeUI : SceneUI
    {
        [SerializeField] private Button _stopRecordingButton;

        private Subject<Unit> _stopRecordingSignalSubj;
        public Observable<Unit> StopRecordingSignal => _stopRecordingSignalSubj;

        private void Awake()
        {
            _stopRecordingSignalSubj = new Subject<Unit>();
            _stopRecordingButton.onClick.AddListener(() => 
                _stopRecordingSignalSubj.OnNext(Unit.Default));
        }
    }
}
