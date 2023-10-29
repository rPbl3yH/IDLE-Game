using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerUnloadResourceMechanics
    {
        private readonly IAtomicVariable<bool> _canUnloadResources;
        private readonly IAtomicVariable<ResourceStorageModel> _resourceStorageModel;
        private readonly Transform _root;
        private readonly DistanceSensor _distanceSensor;

        public PlayerUnloadResourceMechanics(
            IAtomicValue<float> unloadingDistance, 
            IAtomicVariable<bool> canUnloadResources, 
            IAtomicVariable<ResourceStorageModel> resourceStorageModel, 
            Transform root)
        {
            _canUnloadResources = canUnloadResources;
            _resourceStorageModel = resourceStorageModel;
            _root = root;
            _distanceSensor = new DistanceSensor(unloadingDistance);
        }

        public void OnEnable()
        {
            _resourceStorageModel.OnChanged += ResourceStorageModelOnChanged;
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        public void OnDisable()
        {
            _resourceStorageModel.OnChanged -= ResourceStorageModelOnChanged;
            _distanceSensor.Entered -= DistanceSensorOnEntered;
            _distanceSensor.Exited -= DistanceSensorOnExited;
        }

        private void ResourceStorageModelOnChanged(ResourceStorageModel resourceStorage)
        {
            _distanceSensor.SetPoints(_root, resourceStorage.UnloadingPoint);
        }

        private void DistanceSensorOnExited()
        {
            _canUnloadResources.Value = false;
        }

        private void DistanceSensorOnEntered()
        {
            _canUnloadResources.Value = true;
        }

        public void Update()
        {
            _distanceSensor.Update();
        }
    }
}