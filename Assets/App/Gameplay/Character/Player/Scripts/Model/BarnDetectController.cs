using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class BarnDetectController : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private ColliderSensor _colliderSensor;

        private void OnEnable()
        {
            _colliderSensor.ColliderUpdated += OnColliderUpdated;
        }

        private void OnDisable()
        {
            _colliderSensor.ColliderUpdated -= OnColliderUpdated;
        }

        private void OnColliderUpdated(Collider[] colliders)
        {
            foreach (var collider1 in colliders)
            {
                if (collider1 == null)
                {
                    continue;
                }
                
                if (collider1.TryGetComponent(out LevelStorageModel levelStorageModel))
                {
                    _characterModel.LevelStorage.Value = levelStorageModel;
                }
            }
        }
    }
}