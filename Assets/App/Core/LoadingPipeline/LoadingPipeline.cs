using System.Collections.Generic;
using System.Linq;

namespace App.Core
{
    public class LoadingPipeline 
    {
        private readonly List<ILoadingTask> _tasks;

        public LoadingPipeline(IEnumerable<ILoadingTask> tasks)
        {
            _tasks = tasks.ToList();
        }

        public async void Run()
        {
            foreach (var task in _tasks)
            {
                await task.Run();
            }
        }
    }
}