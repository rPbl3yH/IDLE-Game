using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Meta
{
    public class HintTextObserver : IInitializable
    {
        [Inject] 
        private HintTextView _hintTextView;
        
        public void Show(string text)
        {
            _hintTextView.Show(text);    
        }

        public void Hide()
        {
            _hintTextView.Hide();
        }

        public void Initialize()
        {
            _hintTextView.transform
                .DOPunchScale(Vector3.one * 0.2f, 1f, 0)
                .SetLoops(-1)
                .SetLink(_hintTextView.gameObject);
        }
    }
}