using App.Gameplay.Character.Scripts.Model;
using UnityEngine;

namespace App.UI
{
    public class CharacterResourceViewObserver : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private ResourceView _resourceViewPrefab;
        
        private ResourceView _resourceView;

        private void Start()
        {
            _resourceView = Instantiate(_resourceViewPrefab, transform);
            OnAmountChanged(_characterModel.Amount.Value);
        }

        private void OnEnable()
        {
            _characterModel.Amount.OnChanged += OnAmountChanged;
        }

        private void OnDisable()
        {
            _characterModel.Amount.OnChanged -= OnAmountChanged;
        }

        private void OnAmountChanged(int value)
        {
            if (value > 0)
            {
                _resourceView.gameObject.SetActive(true);
                var text = $"{_characterModel.ResourceType.Value} {value}";
                _resourceView.Show(text);    
            }
            else
            {
                _resourceView.Hide();
            }
        }
    }
}