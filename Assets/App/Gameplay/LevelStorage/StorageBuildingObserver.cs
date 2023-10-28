using System;
using App.UI;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class StorageBuildingObserver : MonoBehaviour
    {
        private ResourceViewObserver _resourceViewObserver;
        
        [SerializeField] private Transform _parent;
        [SerializeField] private ResourceView _resourceView;
        [SerializeField] private BuildingModel _buildingModel;

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
                
            } 
        }
    }
}