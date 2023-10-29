using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class LoadResourceViewController : MonoBehaviour
    {
        public event Action<ResourceType> ResourceSelected;
        
        [SerializeField] private LoadResourceView _prefab;
        [SerializeField] private Transform _parent;
        
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
                view.Value.ResourceSelected += ValueOnResourceSelected;
                view.Value.Show(view.Key.ToString());
            }
        }

        public void Hide()
        {
            foreach (var view in _views)
            {
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