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
            if (sceneName != Scenes.BOOT)
                SceneManager.LoadScene(Scenes.BOOT);
        }
    }
}