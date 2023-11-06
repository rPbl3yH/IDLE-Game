using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace App.Core
{
    public class SceneLoader
    {
        private const string GAME_SCENE_NAME = "GameScene";
        private const string LOADING_SCENE_NAME = "LoadingScene";
        private const string SCENE_EXTENSION = ".unity";
        private const string SCENE_PATH = "Assets/App/Scenes/";
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        [MenuItem("Game/Open Loading Scene")]
        public static void OpenLoadingScene()
        {
            OpenScene(LOADING_SCENE_NAME);
        }
        
        [MenuItem("Game/Open Game Scene")]
        public static void OpenGameScene()
        {
            OpenScene(GAME_SCENE_NAME);
        }

        private static void OpenScene(string name)
        {
            EditorSceneManager.OpenScene(SCENE_PATH + name + SCENE_EXTENSION);
        }
    }
}