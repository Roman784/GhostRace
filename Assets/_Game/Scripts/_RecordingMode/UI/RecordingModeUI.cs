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

        private bool _isStopRecordingButtonEnabled = true;

        public Observable<Unit> StopRecordingSignal => _stopRecordingSignalSubj;
        public Observable<Unit> StartRecordingSignal => _startRecordingSignalSubj;

        private void Awake()
        {
            _stopRecordingSignalSubj = new Subject<Unit>();
            _startRecordingSignalSubj = new Subject<Unit>();

            _stopRecordingButton.onClick.AddListener(() =>
                InvokeStopRecordingSignal());

            _startRecordingButton.onClick.AddListener(() =>
                InvokeStartRecordingSignal());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                InvokeStopRecordingSignal();

            if (Input.GetKeyDown(KeyCode.Return))
                InvokeStartRecordingSignal();
        }

        public void EnableStopRecordingButton()
        {
            _isStopRecordingButtonEnabled = true;
            _stopRecordingButton
                .GetComponent<CanvasGroup>()
                .DOFade(1f, 0.25f).SetEase(Ease.OutQuad);
        }

        public void DisableStopRecordingButton()
        {
            _isStopRecordingButtonEnabled = false;
            _stopRecordingButton
                .GetComponent<CanvasGroup>()
                .DOFade(0.5f, 0.25f).SetEase(Ease.OutQuad);
        }

        private void InvokeStopRecordingSignal()
        {
            if (!_isStopRecordingButtonEnabled) return;

            _stopRecordingSignalSubj.OnNext(Unit.Default);
            _stopRecordingSignalSubj.OnCompleted();
        }

        private void InvokeStartRecordingSignal()
        {
            _startRecordingSignalSubj.OnNext(Unit.Default);
            _startRecordingSignalSubj.OnCompleted();

            // Button disappearance animation.
            _startRecordingButton.GetComponent<RectTransform>()
                .DOAnchorPosY(0f, 0.5f)
                .SetEase(Ease.InBack);
        }
    }
}
