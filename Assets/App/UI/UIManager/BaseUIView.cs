using System;
using UnityEngine;

namespace App.UI.UIManager
{
    public abstract class BaseUIView : MonoBehaviour
    {
        public event Action Showed;
        public event Action Hidden;
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
            Showed?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            Hidden?.Invoke();
        }
    }
}