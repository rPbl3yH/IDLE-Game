using System;
using System.Collections.Generic;
using App.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BuildingViewObserver : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private BuildingModel _buildingModel;
        
        [SerializeField] 
        private ResourceView _resourceViewPrefab;
        
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();
        
        private ResourceStorage _resourceStorage;
        
        [Inject]
        public void Construct()
        {
            _resourceStorage = _buildingModel.ResourceStorage;
            _resourceStorage.ResourcesChanged += OnResourcesChanged;
            _buildingModel.Built.AddListener(OnBuilt);
            InitViews();
        }

        private void OnBuilt(Building obj)
        {
            Destroy(gameObject);
        }

        private void InitViews()
        {
            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                var view = Instantiate(_resourceViewPrefab, transform);
                _resourceViews.Add(view);    
            }
            
            HideAll();
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            HideAll();

            foreach (var resource in resources)
            {
                var text = $"{resource.Key} {resource.Value.Amount}";
                _resourceViews[(int)resource.Key].Show(text);
            }
        }

        private void HideAll()
        {
            foreach (var resourceView in _resourceViews)
            {
                resourceView.Hide();
            }
        }

        public void Dispose()
        {
            _resourceStorage.ResourcesChanged -= OnResourcesChanged;
        }
    }
}