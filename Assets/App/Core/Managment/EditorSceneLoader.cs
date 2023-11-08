#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace App.Core
{
    public class EditorSceneLoader
    {
        private const string SCENE_EXTENSION = ".unity";
        private const string SCENE_PATH = "Assets/App/Scenes/";
        
        [MenuItem("Game/Open Loading Scene")]
        public static void OpenLoadingScene()
        {
            OpenScene(SceneLoader.LOADING_SCENE_NAME);
        }
        
        [MenuItem("Game/Open Game Scene")]
        public static void OpenGameScene()
        {
            OpenScene(SceneLoader.GAME_SCENE_NAME);
        }

        private static void OpenScene(string name)
        {
            EditorSceneManager.OpenScene(SCENE_PATH + name + SCENE_EXTENSION);
        }
    }
}
#endif