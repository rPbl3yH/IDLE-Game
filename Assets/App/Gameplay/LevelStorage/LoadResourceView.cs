using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Gameplay.LevelStorage
{
    public class LoadResourceView : BaseView
    {
        public event Action<LoadResourceView> ResourceSelected;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Show(string text)
        {
            _text.text = text;
            base.Show();
        }
        
        private void OnClick()
        {
            ResourceSelected?.Invoke(this);
        }
    }
}