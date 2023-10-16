using VContainer;

namespace App.Core
{
    public class ApplicationLoader
    {
        [Inject]
        private readonly SceneLoader _sceneLoader;

        public void Run()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}