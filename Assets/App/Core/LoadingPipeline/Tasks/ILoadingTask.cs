using System.Threading.Tasks;

namespace App.Core
{
    public interface ILoadingTask
    {
        public Task Run();
    }
}