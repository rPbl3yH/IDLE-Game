using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Panel
{
    public class GameMenuButton : MonoBehaviour
    {
        public event Action Clicked;
        
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}