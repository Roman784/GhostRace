using R3;
using System;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        private readonly float[] _timePoints;
        private readonly CompositeDisposable _disposables = new();

        private Subject<float> _timerSignalsSubj;
        public Observable<float> TimerSignals => _timerSignalsSubj?.AsObservable() ?? Observable.Never<float>();

        public Timer(params float[] timePoints)
        {
            _timePoints = (float[])timePoints.Clone();
            Array.Sort(_timePoints);

            _timerSignalsSubj = new Subject<float>().AddTo(_disposables);
        }

        public Observable<float> Start()
        {
            var time = 0f;
            var currentIdx = 0;

            Observable.EveryUpdate()
                .TakeWhile(_ => currentIdx < _timePoints.Length)
                .Subscribe(_ =>
                {
                    time += Time.deltaTime;

                    if (time >= _timePoints[currentIdx])
                    {
                        _timerSignalsSubj.OnNext(_timePoints[currentIdx]);
                        currentIdx++;

                        if (currentIdx >= _timePoints.Length)
                            _timerSignalsSubj.OnCompleted();
                    }
                })
                .AddTo(_disposables);

            return TimerSignals;
        }

        public void Stop()
        {
            _disposables.Clear();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
