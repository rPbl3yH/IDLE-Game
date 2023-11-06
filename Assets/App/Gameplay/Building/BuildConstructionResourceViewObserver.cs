using System.Collections.Generic;
using System.Linq;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using UnityEngine;
using VContainer;

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
            foreach (var resource in _model.ResourceStorage.Config.Resources)
            {
                var view = Instantiate(_prefab, transform);
                var text = $"{0}/{resource.Count}";
                _resourceViews.Add(view);

                var icon = _iconService.GetIcon(resource.Type);
                
                view.Show(icon, text);
            }
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            foreach (var resource in _model.ResourceStorage.Config.Resources)
            {
                if (!resources.ContainsKey(resource.Type))
                {
                    Debug.LogWarning("No resource in config");
                    return;
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