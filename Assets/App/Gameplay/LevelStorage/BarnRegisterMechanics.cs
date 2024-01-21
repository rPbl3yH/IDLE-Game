using App.Gameplay.Building.Barn;
using Atomic.Elements;

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
            _built.Subscribe(OnBuilt);
        }

        public void OnDisable()
        {
            _built.Unsubscribe(OnBuilt);
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