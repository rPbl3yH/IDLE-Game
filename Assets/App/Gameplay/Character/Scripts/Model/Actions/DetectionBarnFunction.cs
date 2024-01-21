using System;
using App.Gameplay.LevelStorage;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace App.Gameplay.Character.Scripts.Model.Actions
{
    [Serializable]
    public class DetectionBarnFunction : IAtomicFunction<ResourceStorageModel>
    {
        private readonly BarnService _resourceService;

        public DetectionBarnFunction(BarnService resourceService)
        {
            _resourceService = resourceService;
        }
        
        [Button]
        public ResourceStorageModel Invoke()
        {
            return _resourceService.GetStorage();
        }
    }
}