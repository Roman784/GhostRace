using DG.Tweening;
using R3;
using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownUI : FullscreenUI
    {
        [SerializeField] private TMP_Text _view;

        [Space]

        [SerializeField] private float _punchScale;

        public void BindView(int initialValue, Observable<float> timerSignals)
        {
            _view.text = initialValue.ToString();

            timerSignals.Subscribe(timePoint =>
            {
                var time = initialValue - (int)timePoint;
                _view.text = time.ToString();

                _view.transform.localScale = Vector3.one;
                _view.transform.DOPunchScale(Vector3.one * _punchScale, 0.5f, 2, 1f)
                    .SetEase(Ease.OutQuad);

                if (time <= 0)
                {
                    _view.text = "GOOO!";
                    Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
                        _view.DOFade(0, 0.25f).SetEase(Ease.InCirc).OnComplete(() =>
                            _view.gameObject.SetActive(false)
                        )
                    );
                }
            });
        }
    }
}
