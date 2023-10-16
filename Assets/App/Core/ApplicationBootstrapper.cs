using UnityEngine;
using VContainer;

namespace App.Core
{
    public class ApplicationBootstrapper : MonoBehaviour
    {
        [Inject] 
        private ApplicationLoader _applicationLoader;

        private void Start()
        {
            _applicationLoader.Run();
        }
    }
}