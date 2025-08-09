using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameRoot
{
    public class SceneLoader
    {
        public SceneLoader()
        {
        }

        public void LoadAndRunScene<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            Coroutines.StopAll();
            Coroutines.Start(
                LoadAndRunSceneRoutine<TEntryPoint, TEnterParams>(sceneName, enterParams));
        }

        private IEnumerator LoadAndRunSceneRoutine<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            yield return LoadScene(sceneName);

            var sceneEntryPoint = Object.FindFirstObjectByType<TEntryPoint>();
            yield return sceneEntryPoint.Run(enterParams);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }
    }
}