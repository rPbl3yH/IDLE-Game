using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildObserverMechanics
    {
        private readonly AtomicEvent<BuildingModel> _built;
        private readonly ResourceStorageModelService _resourceStorageModelService;
        private readonly GameObject _spawnPoint;
        private readonly BuildingConstructionModel _buildingConstructionModel;
        private readonly GameObject _root;

        public BuildObserverMechanics(
            BuildingConstructionModel buildingConstructionModel,
            AtomicEvent<BuildingModel> built,
            ResourceStorageModelService resourceStorageModelService,
            GameObject spawnPoint)
        {
            _buildingConstructionModel = buildingConstructionModel;
            _built = built;
            _resourceStorageModelService = resourceStorageModelService;
            _spawnPoint = spawnPoint;
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
            _resourceStorageModelService.RemoveStorage(_buildingConstructionModel);
            _spawnPoint.SetActive(false);
            
            if (buildingModel is ResourceStorageModel storageModel)
            {
                _resourceStorageModelService.AddStorage(storageModel);
            }
        }
    }
}