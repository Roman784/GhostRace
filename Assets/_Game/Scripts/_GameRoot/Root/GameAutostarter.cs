using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRoot
{
    // Automatically redirects the game launch to the boot scene,
    // regardless of which one it was launched from.
    public class GameAutostarter
    {
        public static string LaunchedScene {  get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            LaunchedScene = sceneName;

            if (sceneName != Scenes.BOOT)
                SceneManager.LoadScene(Scenes.BOOT);
        }
    }
}