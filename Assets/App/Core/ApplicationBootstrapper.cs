using UnityEngine;
using VContainer;

namespace App.Core
{
    public class ApplicationBootstrapper : MonoBehaviour
    {
        [Inject] 
        private LoadingPipeline _loadingPipeline;
        
        private void Start()
        {
            _loadingPipeline.Run();
        }
    }
}