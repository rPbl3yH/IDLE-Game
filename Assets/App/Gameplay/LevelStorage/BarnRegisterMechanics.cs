using App.Core.SaveSystem.Mediators.Content;
using App.Gameplay.Building.Barn;
using Modules.Atomic.Actions;

namespace App.Gameplay.LevelStorage
{
    public class BarnRegisterMechanics
    {
        private readonly AtomicEvent<BuildingModel> _built;
        private readonly BarnModelService _barnModelService;

        public BarnRegisterMechanics(AtomicEvent<BuildingModel> built, BarnModelService barnModelService)
        {
            _built = built;
            _barnModelService = barnModelService;
        }

        public void OnEnable()
        {
            _built.AddListener(OnBuilt);
        }

        public void OnDisable()
        {
            _built.RemoveListener(OnBuilt);
        }

        private void OnBuilt(BuildingModel buildingModel)
        {
            if (buildingModel is BarnModel barnModel)
            {
                _barnModelService.AddService(barnModel);
            }
        }
    }
}