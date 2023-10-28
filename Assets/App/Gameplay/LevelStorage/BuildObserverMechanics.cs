using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildObserverMechanics
    {
        private readonly AtomicEvent<Building> _built;
        private readonly ResourceStorageModelService _resourceStorageModelService;
        private readonly GameObject _spawnPoint;
        private readonly BuildingModel _buildingModel;
        private readonly GameObject _buildingUI;

        public BuildObserverMechanics(
            BuildingModel buildingModel,
            AtomicEvent<Building> built,
            ResourceStorageModelService resourceStorageModelService,
            GameObject spawnPoint, 
            GameObject buildingUI)
        {
            _buildingModel = buildingModel;
            _built = built;
            _resourceStorageModelService = resourceStorageModelService;
            _spawnPoint = spawnPoint;
            _buildingUI = buildingUI;
        }

        public void OnEnable()
        {
            _built.AddListener(OnBuilt);
        }

        public void OnDisable()
        {
            _built.RemoveListener(OnBuilt);
        }

        private void OnBuilt(Building building)
        {
            _resourceStorageModelService.RemoveStorage(_buildingModel);
            _buildingUI.SetActive(false);
            _spawnPoint.SetActive(false);

            if (building is ResourceStorageModel storageModel)
            {
                _resourceStorageModelService.AddStorage(storageModel);
            }
        }
    }
}