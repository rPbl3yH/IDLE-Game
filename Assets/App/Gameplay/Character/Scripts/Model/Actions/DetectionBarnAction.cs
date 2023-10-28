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
        private readonly IAtomicVariable<ResourceStorage> _barnModel;

        public DetectionBarnAction(
            IAtomicVariable<ResourceStorage> barnModel,
            BarnService resourceService)
        {
            _barnModel = barnModel;
            _resourceService = resourceService;
        }

        [Button]
        public void Invoke()
        {
            _barnModel.Value = _resourceService.GetStorage().ResourceStorage;
        }
    }
}