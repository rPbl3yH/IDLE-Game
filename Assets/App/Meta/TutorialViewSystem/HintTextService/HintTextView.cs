using System;
using TMPro;
using UnityEngine;

namespace App.Meta
{
    public class HintTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            Hide();
        }

        public void Show(string text)
        {
            _text.text = text;
        }

        public void Hide()
        {
            _text.text = "";
        }
    }
}