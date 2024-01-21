using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceMechanics
    {
        private readonly AtomicEvent<ResourceType> _resourceSelected;
        private readonly AtomicVariable<bool> _isFreeSpace;
        private readonly IAtomicVariable<bool> _canUnload;
        private readonly IAtomicVariable<ResourceType> _loadResourceType;
        private readonly IAtomicVariable<bool> _canLoad;

        public PlayerLoadResourceMechanics(
            AtomicEvent<ResourceType> resourceSelected, 
            AtomicVariable<bool> isFreeSpace, 
            IAtomicVariable<bool> canUnload, 
            IAtomicVariable<ResourceType> loadResourceType, 
            IAtomicVariable<bool> canLoad)
        {
            _isFreeSpace = isFreeSpace;
            _canUnload = canUnload;
            _resourceSelected = resourceSelected;
            _loadResourceType = loadResourceType;
            _canLoad = canLoad;
        }

        private void OnLoadResourceSelected(ResourceType resourceType)
        {
            Debug.Log(resourceType);
            _loadResourceType.Value = resourceType;
            _canUnload.Value = false;
            _canLoad.Value = true;
        }

        public void OnEnable()
        {
            _resourceSelected.Subscribe(OnLoadResourceSelected);
            _isFreeSpace.Subscribe(IsFreeSpaceOnChanged);
        }

        public void OnDisable()
        {
            _resourceSelected.Unsubscribe(OnLoadResourceSelected);
            _isFreeSpace.Unsubscribe(IsFreeSpaceOnChanged);
        }
        
        private void IsFreeSpaceOnChanged(bool value)
        {
            if (!value)
            {
                Debug.Log("Free space changed");
                _canLoad.Value = false;
            }
        }
    }
}