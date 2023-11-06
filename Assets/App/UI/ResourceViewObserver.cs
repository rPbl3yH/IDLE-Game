using System;
using System.Collections.Generic;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.UI
{
    public class ResourceViewObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourceStorageModel _model;

        [SerializeField] 
        private ResourceView _prefab;

        [SerializeField] 
        private ResourceIconConfig _config;
        
        private readonly List<ResourceView> _resourceViews = new();

        private void Awake()
        {
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
                Sprite sprite = null;
                
                if (_config.Sprites.TryGetValue(resource.Key, out var icon))
                {
                    sprite = icon;
                }
                
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
