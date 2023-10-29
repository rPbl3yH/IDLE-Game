using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceAvailableMechanics 
    {
        private readonly AtomicVariable<bool> _canShowLoadResources;
        private readonly AtomicVariable<ResourceType> _loadResourceType;
        private readonly AtomicEvent<ResourceType> _resourceSelected;
        private readonly IAtomicVariable<bool> _isFreeSpace;
        private readonly IAtomicValue<ResourceStorageModel> _resourceStorage;
        private readonly IAtomicVariable<bool> _canLoad;
        private readonly IAtomicVariable<bool> _canUnload;
        private readonly IAtomicValue<int> _amount;
        private readonly Transform _root;
        
        private readonly DistanceSensor _distanceSensor;

        public PlayerLoadResourceAvailableMechanics(
            AtomicVariable<bool> canShowLoadResources,
            AtomicVariable<float> loadingDistance, 
            AtomicVariable<ResourceType> loadResourceType,
            AtomicEvent<ResourceType> resourceSelected,
            IAtomicVariable<bool> isFreeSpace,
            IAtomicValue<ResourceStorageModel> resourceStorage,
            IAtomicVariable<bool> canLoad,
            IAtomicVariable<bool> canUnload,
            IAtomicValue<int> amount,
            Transform root)
        {
            _canShowLoadResources = canShowLoadResources;
            _loadResourceType = loadResourceType;
            _resourceSelected = resourceSelected;
            _isFreeSpace = isFreeSpace;
            _resourceStorage = resourceStorage;
            _canLoad = canLoad;
            _canUnload = canUnload;
            _amount = amount;
            _root = root;
            _distanceSensor = new DistanceSensor(loadingDistance);
        }
        
        public PlayerLoadResourceAvailableMechanics(PlayerModel playerModel)
        {
            _canShowLoadResources = playerModel.IsShowLoadResources;
            _loadResourceType = playerModel.CharacterModel.LoadResourceType;
            _resourceSelected = playerModel.LoadResourceSelected;
            _isFreeSpace = playerModel.CharacterModel.IsFreeSpace;
            _resourceStorage = playerModel.CharacterModel.ResourceStorage;
            _canLoad = playerModel.CharacterModel.CanLoadResources;
            _canUnload = playerModel.CharacterModel.CanUnloadResources;
            _amount = playerModel.CharacterModel.ResourceAmount;
            _root = playerModel.CharacterModel.Root;
            _distanceSensor = new DistanceSensor(playerModel.CharacterModel.LoadingDistance);
        }

        public void OnEnable()
        {
            _resourceSelected.AddListener(OnLoadResourceSelected);
            _isFreeSpace.OnChanged += IsFreeSpaceOnChanged;
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        public void OnDisable()
        {
            _resourceSelected.RemoveListener(OnLoadResourceSelected);
            _isFreeSpace.OnChanged -= IsFreeSpaceOnChanged;
            _distanceSensor.Entered -= DistanceSensorOnEntered;
            _distanceSensor.Exited -= DistanceSensorOnExited;
        }

        private void DistanceSensorOnExited()
        {
            _canLoad.Value = false;
            _canShowLoadResources.Value = false;
        }

        private void DistanceSensorOnEntered()
        {
            Debug.Log("Entered");
            if (_amount.Value == 0)
            {
                _canShowLoadResources.Value = true;
            }
        }

        private void IsFreeSpaceOnChanged(bool value)
        {
            if (!value)
            {
                Debug.Log("Free space changed");
                _canLoad.Value = false;
            }
        }

        private void OnLoadResourceSelected(ResourceType resourceType)
        {
            Debug.Log(resourceType);
            _loadResourceType.Value = resourceType;
            _canUnload.Value = false;
            _canLoad.Value = true;
        }

        public void Update()
        {
            _distanceSensor.SetPoints(_root, _resourceStorage.Value.UnloadingPoint);
            _distanceSensor.Update();
        }
    }
}