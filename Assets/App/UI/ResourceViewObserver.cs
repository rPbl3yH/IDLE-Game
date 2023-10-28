using System;
using System.Collections.Generic;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.UI
{
    public class ResourceViewObserver : MonoBehaviour
    {
        [SerializeField] private ResourceStorageModel _resourceStorageModel;
        [SerializeField] private ResourceView _resourceViewPrefab;
        
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();

        private void OnEnable()
        {
            _resourceStorageModel.ResourceStorage.ResourcesChanged += OnResourcesChanged;
        }

        private void OnDisable()
        {
            _resourceStorageModel.ResourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void Start()
        {
            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                _resourceViews.Add(Instantiate(_resourceViewPrefab, transform));    
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
    }
}
