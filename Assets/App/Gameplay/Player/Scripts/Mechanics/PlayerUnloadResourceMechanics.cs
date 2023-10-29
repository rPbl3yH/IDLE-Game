using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerUnloadResourceMechanics
    {
        private readonly CharacterModel _characterModel;
        private readonly IAtomicValue<ResourceStorageModel> _resourceStorageModel;
        private readonly DistanceSensor _distanceSensor;

        public PlayerUnloadResourceMechanics(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _resourceStorageModel = characterModel.ResourceStorage;
            _distanceSensor = new DistanceSensor(_characterModel.UnloadingDistance);
        }

        public void OnEnable()
        {
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        public void OnDisable()
        {
            _distanceSensor.Entered -= DistanceSensorOnEntered;
            _distanceSensor.Exited -= DistanceSensorOnExited;
        }

        private void DistanceSensorOnExited()
        {
            Debug.Log("exited unload");
            _characterModel.CanUnloadResources.Value = false;
        }

        private void DistanceSensorOnEntered()
        {
            Debug.Log("entered unload");
            _characterModel.CanUnloadResources.Value = true;
        }

        public void Update()
        {
            _distanceSensor.SetPoints(_characterModel.Root, _resourceStorageModel.Value.UnloadingPoint);
            _distanceSensor.Update();
        }
    }
}