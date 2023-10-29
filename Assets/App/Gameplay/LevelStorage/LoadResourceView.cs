using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Gameplay.LevelStorage
{
    public class LoadResourceView : MonoBehaviour
    {
        public event Action<ResourceType> ResourceSelected;
        
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _text.text = _resourceType.ToString();
        }

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
            ResourceSelected?.Invoke(_resourceType);
        }
    }
}