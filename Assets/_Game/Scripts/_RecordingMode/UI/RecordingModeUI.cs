using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecordingModeUI : SceneUI
    {
        [SerializeField] private Button _stopRecordingButton;
        [SerializeField] private Button _startRecordingButton;

        private Subject<Unit> _stopRecordingSignalSubj;
        private Subject<Unit> _startRecordingSignalSubj;

        public Observable<Unit> StopRecordingSignal => _stopRecordingSignalSubj;
        public Observable<Unit> StartRecordingSignal => _startRecordingSignalSubj;

        private void Awake()
        {
            _stopRecordingSignalSubj = new Subject<Unit>();
            _startRecordingSignalSubj = new Subject<Unit>();

            _stopRecordingButton.onClick.AddListener(() => 
                _stopRecordingSignalSubj.OnNext(Unit.Default));

            _startRecordingButton.onClick.AddListener(() =>
            {
                _startRecordingSignalSubj.OnNext(Unit.Default);

                // Button disappearance animation.
                _startRecordingButton.GetComponent<RectTransform>()
                    .DOAnchorPosY(0f, 0.5f)
                    .SetEase(Ease.InBack);
            });
        }
    }
}
