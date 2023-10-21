using UnityEngine.SceneManagement;

namespace App.Core
{
    public class SceneLoader
    {
        public const string GameSceneName = "GameScene";

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}