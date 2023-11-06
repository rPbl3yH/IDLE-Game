using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Gameplay.LevelStorage
{
    public class LoadResourceView : BaseView
    {
        public event Action<LoadResourceView> ResourceSelected;

        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            ResourceSelected?.Invoke(this);
        }
    }
}