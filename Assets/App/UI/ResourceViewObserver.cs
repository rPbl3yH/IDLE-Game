using System;
using System.Collections.Generic;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace App.UI
{
    public class ResourceViewObserver : MonoBehaviour
    {
        [SerializeField] private LevelStorageModel _levelStorageModel;
        [SerializeField] private ResourceView _resourceViewPrefab;
        
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();

        private void OnEnable()
        {
            _levelStorageModel.ResourceStorage.ResourceChanged += OnResourceChanged;
        }

        private void OnDisable()
        {
            _levelStorageModel.ResourceStorage.ResourceChanged -= OnResourceChanged;
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

        private void OnResourceChanged(Dictionary<ResourceType, int> resources)
        {
            HideAll();

            for (int i = 0; i < resources.Count; i++)
            {
                var resourceType = (ResourceType)i;
                var text = $"{resourceType} {resources[resourceType]}";
                _resourceViews[i].Show(text);
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
