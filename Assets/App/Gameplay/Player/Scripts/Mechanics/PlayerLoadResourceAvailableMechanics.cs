using App.Gameplay.LevelStorage;
using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceAvailableMechanics 
    {
        private readonly IAtomicVariable<bool> _canShowLoadResources;
        private readonly AtomicVariable<ResourceStorageModel> _resourceStorage;
        private readonly IAtomicVariable<bool> _canLoad;
        private readonly IAtomicValue<int> _amount;
        private readonly AtomicEvent<ResourceType> _resourceUnloaded;
        private readonly Transform _root;
        
        private readonly DistanceSensor _distanceSensor;

        public PlayerLoadResourceAvailableMechanics(
            IAtomicVariable<bool> canShowLoadResources,
            IAtomicValue<float> loadingDistance, 
            AtomicVariable<ResourceStorageModel> resourceStorage,
            IAtomicVariable<bool> canLoad,
            IAtomicValue<int> amount,
            Transform root)
        {
            _canShowLoadResources = canShowLoadResources;
            _resourceStorage = resourceStorage;
            _canLoad = canLoad;
            _amount = amount;
            _root = root;
            _distanceSensor = new DistanceSensor(loadingDistance);
        }
        
        public PlayerLoadResourceAvailableMechanics(PlayerModel playerModel)
        {
            _canShowLoadResources = playerModel.IsShowLoadResources;
            _resourceStorage = playerModel.CharacterModel.ResourceStorage;
            _canLoad = playerModel.CharacterModel.CanLoadResources;
            _amount = playerModel.CharacterModel.ResourceAmount;
            _root = playerModel.CharacterModel.Root;
            _resourceUnloaded = playerModel.CharacterModel.ResourceUnloaded;
            _distanceSensor = new DistanceSensor(playerModel.CharacterModel.LoadingDistance);
        }

        public void OnEnable()
        {
            _resourceUnloaded.Subscribe(OnResourceUnloaded);
            _resourceStorage.Subscribe(ResourceStorageOnChanged);
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        public void OnDisable()
        {
            _resourceUnloaded.Unsubscribe(OnResourceUnloaded);
            _resourceStorage.Unsubscribe(ResourceStorageOnChanged);
            _distanceSensor.Entered -= DistanceSensorOnEntered;
            _distanceSensor.Exited -= DistanceSensorOnExited;
        }

        private void ResourceStorageOnChanged(ResourceStorageModel value)
        {
            if (value == null)
            {
                return;
            }
            
            _distanceSensor.SetPoints(_root, value.UnloadingPoint);
        }

        private void DistanceSensorOnExited()
        {
            _canLoad.Value = false;
            _canShowLoadResources.Value = false;
        }

        private void OnResourceUnloaded(ResourceType type)
        {
            DistanceSensorOnEntered();
        }

        private void DistanceSensorOnEntered()
        {
            if (_amount.Value == 0)
            {
                _canShowLoadResources.Value = true;
            }
        }

        public void Update()
        {
            _distanceSensor.Update();
        }
    }
}