using App.Gameplay.LevelStorage;
using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerUnloadResourceMechanics
    {
        private readonly IAtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<ResourceStorageModel> _resourceStorageModel;
        private readonly Transform _root;
        private readonly DistanceSensor _distanceSensor;

        public PlayerUnloadResourceMechanics(
            IAtomicValue<float> unloadingDistance, 
            IAtomicVariable<bool> canUnloadResources, 
            AtomicVariable<ResourceStorageModel> resourceStorageModel, 
            Transform root)
        {
            _canUnloadResources = canUnloadResources;
            _resourceStorageModel = resourceStorageModel;
            _root = root;
            _distanceSensor = new DistanceSensor(unloadingDistance);
        }

        public void OnEnable()
        {
            _resourceStorageModel.Subscribe(ResourceStorageModelOnChanged);
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        public void OnDisable()
        {
            _resourceStorageModel.Unsubscribe(ResourceStorageModelOnChanged);
            _distanceSensor.Entered -= DistanceSensorOnEntered;
            _distanceSensor.Exited -= DistanceSensorOnExited;
        }

        private void ResourceStorageModelOnChanged(ResourceStorageModel resourceStorage)
        {
            if (resourceStorage != null)
            {
                _distanceSensor.SetPoints(_root, resourceStorage.UnloadingPoint);
            }
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