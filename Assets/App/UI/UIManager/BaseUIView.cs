using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.UIManager
{
    public abstract class BaseUIView : MonoBehaviour
    {
        public event Action Showed;
        public event Action Hidden;
        
        [SerializeField] private Image _image;
        [SerializeField] private float _appearanceDuration;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            transform.DOScale(Vector3.one, _appearanceDuration).From(0.5f).SetLink(gameObject);
            _image.DOFade(1f, _appearanceDuration).From(0.5f).SetLink(_image.gameObject);
            Showed?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            Hidden?.Invoke();
        }
    }
}