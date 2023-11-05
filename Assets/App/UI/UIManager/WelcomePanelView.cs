using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.UIManager
{
    public class WelcomePanelView : BaseUIView
    {
        [SerializeField] 
        private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Hide();
        }
    }
}