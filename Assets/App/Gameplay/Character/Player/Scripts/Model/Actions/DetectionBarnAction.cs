using System;
using App.Gameplay.LevelStorage;
using Atomic;
using Sirenix.OdinInspector;

namespace App.Gameplay.AI.States
{
    [Serializable]
    public class DetectionBarnAction : IAtomicAction
    {
        private readonly BarnService _resourceService;
        private readonly IAtomicVariable<BarnModel> _barnModel;

        public DetectionBarnAction(
            IAtomicVariable<BarnModel> barnModel,
            BarnService resourceService)
        {
            _barnModel = barnModel;
            _resourceService = resourceService;
        }

        [Button]
        public void Invoke()
        {
            _barnModel.Value = _resourceService.GetStorage();
        }
    }
}