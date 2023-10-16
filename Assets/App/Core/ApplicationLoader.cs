namespace App.Core
{
    public class ApplicationLoader
    {
        private readonly SceneLoader _sceneLoader;

        public ApplicationLoader(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Run()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}