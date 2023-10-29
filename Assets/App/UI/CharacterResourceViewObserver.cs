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
            OnResourceAmountChanged(_characterModel.ResourceAmount.Value);
        }

        private void OnEnable()
        {
            _characterModel.ResourceAmount.OnChanged += OnResourceAmountChanged;
        }

        private void OnDisable()
        {
            _characterModel.ResourceAmount.OnChanged -= OnResourceAmountChanged;
        }

        private void OnResourceAmountChanged(int value)
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