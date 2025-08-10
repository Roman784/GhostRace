using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRoot
{
    // Automatically redirects the game launch to the boot scene,
    // regardless of which one it was launched from.
    public class GameAutostarter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            var sceneName = SceneManager.GetActiveScene().name;

            if (!IsGameScene(sceneName)) return;

            if (sceneName != Scenes.BOOT)
                SceneManager.LoadScene(Scenes.BOOT);
        }

        // To avoid interrupting the loading of a scene not from the current game when launching.
        // For example, for demo scenes from assets.
        private static bool IsGameScene(string name)
        {
            return name == Scenes.BOOT || 
                   name == Scenes.RACE_MODE || 
                   name == Scenes.RECORDING_MODE;
        }
    }
}