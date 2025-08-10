using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameRoot
{
    public class SceneLoader
    {
        private readonly UIRoot _ui;

        public SceneLoader(UIRoot ui)
        {
            _ui = ui;
        }

        public void LoadAndRunScene<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            Coroutines.StopAll();
            Coroutines.Start(
                LoadAndRunSceneRoutine<TEntryPoint, TEnterParams>(sceneName, enterParams));
        }

        // Sequential loading and starting of a new scene.
        private IEnumerator LoadAndRunSceneRoutine<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            yield return _ui.ShowLoadingScreen();
            
            _ui.ClearAllContainers();

            yield return LoadScene(sceneName);

            var sceneEntryPoint = Object.FindFirstObjectByType<TEntryPoint>();
            yield return sceneEntryPoint.Run(enterParams);

            yield return _ui.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }
    }
}