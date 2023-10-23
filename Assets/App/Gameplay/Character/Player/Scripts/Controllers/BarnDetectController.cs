using System;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class BarnDetectController : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private LevelStorageService _levelStorageService;

        private void Update()
        {
            _characterModel.LevelStorage.Value = _levelStorageService.GetStorage();
        }
    }
}