using Gameplay;
using GameRoot;
using System;
using System.Collections;
using UnityEngine;

namespace RaceMode
{
    public class RaceModeEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Level _level;
        [SerializeField] private Car _car;

        public override IEnumerator Run<T>(T enterParams)
        {
            if (enterParams is RaceModeEnterParams gameplayParams)
                yield return Run(gameplayParams);
            else
                throw new ArgumentException($"Failed to convert {typeof(T)} to {typeof(RaceModeEnterParams)}!");
        }

        // Sequential scene initialization.
        private IEnumerator Run(RaceModeEnterParams enterParams)
        {
            var isLoaded = false;

            _level.PlaceCar(_car);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}
