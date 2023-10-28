using System;
using UnityEngine;

namespace App.UI
{
    public class BaseGameView : MonoBehaviour
    {
        [SerializeField] private Transform _worldTarget;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _offsetY;
        
        private void Update()
        {
            if (Camera.main != null)
            {
                var offset = new Vector3(0f, _offsetY, 0f);
                _rectTransform.position = Camera.main.WorldToScreenPoint(_worldTarget.position) + offset;
            }
        }
    }
}
