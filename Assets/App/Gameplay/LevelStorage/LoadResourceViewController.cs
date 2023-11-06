using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Gameplay.LevelStorage
{
    public class LoadResourceViewController : MonoBehaviour
    {
        public event Action<ResourceType> ResourceSelected;
        
        [SerializeField] private LoadResourceView _prefab;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private ResourceStorageModel _resourceStorage;
        
        [ShowInInspector]
        private Dictionary<ResourceType, LoadResourceView> _views = new();

        private void Awake()
        {
            var resources = Enum.GetValues(typeof(ResourceType)).Length;
            for (int i = 0; i < resources; i++)
            {
                var view = Instantiate(_prefab, _parent);
                _views.Add((ResourceType)i, view);
            }
            
            Hide();
        }

        public void Show()
        {
            foreach (var view in _views)
            {
                if (_resourceStorage.ResourceStorage.Resources.ContainsKey(view.Key))
                {
                    view.Value.ResourceSelected += ValueOnResourceSelected;
                    view.Value.Show();
                }
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_parent);
        }

        public void Hide()
        {
            foreach (var view in _views)
            {
                if (!view.Value.isActiveAndEnabled)
                {
                    continue;
                }
                view.Value.ResourceSelected -= ValueOnResourceSelected;
                view.Value.Hide();
            }
        }

        private void ValueOnResourceSelected(LoadResourceView value)
        {
            var pair = _views.FirstOrDefault(pair => pair.Value == value);
            ResourceSelected?.Invoke(pair.Key);
        }
    }
}