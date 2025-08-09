using GameRoot;
using System;
using System.Collections;
using UnityEngine;

namespace RaceMode
{
    public class RaceModeEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Run<T>(T enterParams)
        {
            if (enterParams is RaceModeEnterParams gameplayParams)
                yield return Run(gameplayParams);
            else
                throw new ArgumentException($"Failed to convert {typeof(T)} to {typeof(RaceModeEnterParams)}!");
        }

        private IEnumerator Run(RaceModeEnterParams enterParams)
        {
            var isLoaded = false;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
