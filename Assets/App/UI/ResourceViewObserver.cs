using System;
using System.Collections.Generic;
using App.Gameplay;
using App.Gameplay.LevelStorage;
using App.Gameplay.ResourceStorage;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace App.UI
{
    public class ResourceViewObserver : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;
        [SerializeField] private ResourceView _resourceViewPrefab;
        
        [ShowInInspector, ReadOnly]
        private List<ResourceView> _resourceViews = new();

        private void OnEnable()
        {
            _barnModel.ResourceStorage.ResourceChanged += OnResourceChanged;
        }

        private void OnDisable()
        {
            _barnModel.ResourceStorage.ResourceChanged -= OnResourceChanged;
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

        private void OnResourceChanged(Dictionary<ResourceType, ResourceValue> resources)
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
