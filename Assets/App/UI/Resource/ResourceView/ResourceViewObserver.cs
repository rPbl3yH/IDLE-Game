using System;
using System.Collections.Generic;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using UnityEngine;
using VContainer;

namespace App.UI
{
    public class ResourceViewObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourceStorageModel _model;

        private ResourceView _prefab;
        private ResourceIconService _iconService;
        private readonly List<ResourceView> _resourceViews = new();

        [Inject]
        public void Construct(ResourceView viewPrefab, ResourceIconService service)
        {
            _prefab = viewPrefab;
            _iconService = service;
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
            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                var view = Instantiate(_prefab, transform);
                _resourceViews.Add(view);    
            }
            
            HideAll();
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            HideAll();

            foreach (var resource in resources)
            {
                var text = $"{resource.Value.Amount}";
                var sprite = _iconService.GetIcon(resource.Key);
                
                _resourceViews[(int)resource.Key].Show(sprite, text);
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
