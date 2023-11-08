using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.UI
{
    public class BuildConstructionResourceViewObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourceStorageModel _model;

        [SerializeField]
        private ResourceView _prefab;
        
        private ResourceIconService _iconService;

        private readonly List<ResourceView> _resourceViews = new();

        [Inject] 
        public void Construct(ResourceView prefab, ResourceIconService resourceIconService)
        {
            _prefab = prefab;
            _iconService = resourceIconService;
        }

        private void OnEnable()
        {
            _model.ResourceStorage.ResourcesChanged += OnResourcesChanged;
        }

        private void OnDisable()
        {
            _model.ResourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        public void Start()
        {
            InitViews();
        }

        private void InitViews()
        {
            foreach (var configResource in _model.ResourceStorage.Config.Resources)
            {
                var view = Instantiate(_prefab, transform);
                if (_model.ResourceStorage.TryGetResource(configResource.Type, out var resourceCount))
                {
                    //TODO: вынести инициализацию
                    
                }
                var text = $"{resourceCount}/{configResource.Count}";
                _resourceViews.Add(view);

                var icon = _iconService.GetIcon(configResource.Type);
                
                view.Show(icon, text);
            }
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            foreach (var resource in _model.ResourceStorage.Config.Resources)
            {
                if (!resources.ContainsKey(resource.Type))
                {
                    //Debug.LogWarning("No resource in config");
                    continue;
                }
                var currentResource = resources.FirstOrDefault(pair => pair.Key == resource.Type);
                var text = $"{currentResource.Value.Amount}/{resource.Count}";
                
                var icon = _iconService.GetIcon(resource.Type);
                _resourceViews[(int)resource.Type].Show(icon, text);
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