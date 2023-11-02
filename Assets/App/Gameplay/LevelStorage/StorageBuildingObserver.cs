using System;
using App.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.LevelStorage
{
    public class StorageBuildingObserver : MonoBehaviour
    {
        [Inject] private IObjectResolver _container;
        [Inject] private ResourceViewFactory _resourceViewFactory;

        [SerializeField] private BuildingModel _buildingModel;
        [SerializeField] private BarnService _barnService;

        private ResourceViewObserver _resourceViewObserver;

        private void OnEnable()
        {
            _buildingModel.Built.AddListener(OnBuilt);
        }

        private void OnDisable()
        {
            _buildingModel.Built.RemoveListener(OnBuilt);
        }

        private void OnBuilt(Building building)
        {
            if (building is ResourceStorageModel resourceStorageModel)
            {
                _barnService.RegisterBarn(resourceStorageModel);
                print("Create resource view observer");
            } 
        }
    }
}