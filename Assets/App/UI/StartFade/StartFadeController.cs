using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.StartFade
{
    public class StartFadeController : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;

        private void Start()
        {
            _image.gameObject.SetActive(true);
            _image.DOFade(0f, _duration).From(1f).SetEase(Ease.InExpo);
        }
    }
}