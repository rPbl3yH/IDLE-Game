using UnityEngine.SceneManagement;

namespace App.Core
{
    public class SceneLoader
    {
        public const string GAME_SCENE_NAME = "GameScene";
        public const string LOADING_SCENE_NAME = "LoadingScene";
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        public void LoadLoadingScene()
        {
            SceneManager.LoadScene(LOADING_SCENE_NAME);
        }
    }
}