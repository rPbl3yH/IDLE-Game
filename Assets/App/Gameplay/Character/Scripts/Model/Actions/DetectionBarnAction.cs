using System;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using Sirenix.OdinInspector;

namespace App.Gameplay.Character.Scripts.Model.Actions
{
    [Serializable]
    public class DetectionBarnAction : IAtomicAction
    {
        private readonly BarnService _resourceService;
        private readonly IAtomicVariable<ResourceStorageModel> _resourceStorageModel;

        public DetectionBarnAction(
            IAtomicVariable<ResourceStorageModel> resourceStorageModel,
            BarnService resourceService)
        {
            _resourceStorageModel = resourceStorageModel;
            _resourceService = resourceService;
        }

        [Button]
        public void Invoke()
        {
            _resourceStorageModel.Value = _resourceService.GetStorage();
        }
    }
}