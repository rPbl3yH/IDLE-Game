using System.Threading.Tasks;
using VContainer;

namespace App.Core
{
    public class LoadingTask_LoadPlayerData : ILoadingTask
    {
        [Inject] 
        private GameRepository _gameRepository; 
    
        public Task Run()
        {
            _gameRepository.LoadState();
            return Task.CompletedTask;
        }
    }
}