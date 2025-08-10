using R3;
using System;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        private readonly float[] _timePoints;
        private readonly CompositeDisposable _disposables = new();

        public Timer(params float[] timePoints)
        {
            _timePoints = timePoints;
            Array.Sort(_timePoints);
        }

        public Observable<float> Start()
        {
            return Observable.Create<float>(observer =>
            {
                var time = 0f;
                var currentIdx = 0;

                return Observable.EveryUpdate()
                    .Subscribe(_ =>
                    {
                        time += Time.deltaTime;

                        var timePoint = _timePoints[currentIdx];
                        if (currentIdx < _timePoints.Length && time >= timePoint)
                        {
                            observer.OnNext(timePoint);
                            currentIdx++;

                            if (currentIdx >= _timePoints.Length)
                                observer.OnCompleted();
                        }
                    })
                    .AddTo(_disposables);
            });
        }

        public void Stop()
        {
            _disposables.Dispose();
        }
    }
}
