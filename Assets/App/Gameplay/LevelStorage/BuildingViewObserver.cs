using System;
using System.Collections.Generic;
using App.UI;
using App.UI.UIManager;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BuildingViewObserver : BaseGameView, IDisposable
    {
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();
        
        [Inject] 
        private ResourceView _resourceViewPrefab;
        
        private ResourceStorage _resourceStorage;
        
        public void Construct(BuildingModel buildingModel)
        {
            _resourceStorage = buildingModel.ResourceStorage;
            _resourceStorage.ResourcesChanged += OnResourcesChanged;
            buildingModel.Built.AddListener(OnBuilt);
            SetTarget(buildingModel.UnloadingPoint);
            Init();
        }

        private void OnBuilt(Building obj)
        {
            Destroy(gameObject);
        }

        private void Init()
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