using System.Threading.Tasks;
using VContainer;

namespace App.Core
{
    public class LoadingTask_LoadGameScene : ILoadingTask
    {
        [Inject] 
        private SceneLoader _sceneLoader;
        
        public Task Run()
        {
            _sceneLoader.LoadGameScene();
            return Task.CompletedTask;
        }
    }
}