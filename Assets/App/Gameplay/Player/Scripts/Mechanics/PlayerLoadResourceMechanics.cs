using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceMechanics
    {
        private readonly AtomicEvent<ResourceType> _resourceSelected;
        private readonly IAtomicVariable<bool> _isFreeSpace;
        private readonly IAtomicVariable<bool> _canUnload;
        private readonly IAtomicVariable<ResourceType> _loadResourceType;
        private readonly IAtomicVariable<bool> _canLoad;

        public PlayerLoadResourceMechanics(
            AtomicEvent<ResourceType> resourceSelected, 
            IAtomicVariable<bool> isFreeSpace, 
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
            _resourceSelected.AddListener(OnLoadResourceSelected);
            _isFreeSpace.OnChanged += IsFreeSpaceOnChanged;
        }

        public void OnDisable()
        {
            _resourceSelected.RemoveListener(OnLoadResourceSelected);
            _isFreeSpace.OnChanged -= IsFreeSpaceOnChanged;
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