using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using App.Gameplay.Player;
using UnityEngine;

namespace App.UI
{
    public class BuildingResourceViewObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourceStorageModel _model;

        [SerializeField] 
        private ResourceView _prefab;

        private ResourceStorageConfig _resourceStorageConfig;

        private readonly List<ResourceView> _resourceViews = new();

        private void Start()
        {
            _resourceStorageConfig = _model.ResourceStorage.StorageConfig;
            InitViews();
        }

        private void OnEnable()
        {
            _model.ResourceStorage.ResourcesChanged += OnResourcesChanged;
        }

        private void OnDisable()
        {
            _model.ResourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void InitViews()
        {
            foreach (var resource in _resourceStorageConfig.Resources)
            {
                var view = Instantiate(_prefab, transform);
                var text = $"{resource.Type} {0}/{resource.Count}";
                _resourceViews.Add(view);  
                view.Show(text);
            }
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            foreach (var resource in _resourceStorageConfig.Resources)
            {
                if (!resources.ContainsKey(resource.Type))
                {
                    Debug.LogWarning("No resource in config");
                    return;
                }
                var currentResource = resources.FirstOrDefault(pair => pair.Key == resource.Type);
                var text = $"{resource.Type} {currentResource.Value.Amount}/{resource.Count}";
                _resourceViews[(int)resource.Type].Show(text);
            }
        }

        private void HideAll()
        {
            foreach (var resourceView in _resourceViews)
            {
                resourceView.Hide();
            }
        }
    }
}