using System;
using System.Collections.Generic;
using App.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildingViewObserver : MonoBehaviour
    {
        [SerializeField] private ResourceStorageModel _resourceStorageModel;
        [SerializeField] private ResourceView _resourceModelViewPrefab;

        private ResourceStorage _resourceStorage;
        
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();
        
        private void Awake()
        {
            if (_resourceStorageModel != null)
            {
                _resourceStorage = _resourceStorageModel.ResourceStorage;
            }
        }

        private void OnEnable()
        {
            _resourceStorage.ResourcesChanged += OnResourcesChanged;
        }

        private void OnDisable()
        {
            _resourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void Start()
        {
            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                _resourceViews.Add(Instantiate(_resourceModelViewPrefab, transform));    
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