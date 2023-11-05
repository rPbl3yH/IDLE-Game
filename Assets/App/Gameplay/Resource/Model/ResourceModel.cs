using System;
using App.Gameplay.Resource.Model.Mechanics;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;
using UnityEngine.AI;

namespace App.Gameplay.Resource.Model
{
    public class ResourceModel : MonoBehaviour
    {
        public ResourceType ResourceType;
        
        public AtomicVariable<int> Amount;
        public AtomicVariable<int> MaxAmount = new(10);
        public AtomicEvent<int> Gathered;
        public AtomicVariable<float> UpdateTime;
        public AtomicVariable<bool> IsEnable;

        public NavMeshObstacle Obstacle;

        private GatheringMechanics _gatheringMechanics;
        private EnableResourceMechanics _enableResourceMechanics;
        private NavMeshDestroyMechanics _destroyMechanics;
        private ResourceUpdateMechanics _resourceUpdateMechanics;
        
        private void Awake()
        {
            Amount.Value = MaxAmount.Value;
            
            _gatheringMechanics = new GatheringMechanics(Gathered, Amount);
            _enableResourceMechanics = new EnableResourceMechanics(IsEnable, Amount);
            _destroyMechanics = new NavMeshDestroyMechanics(Obstacle, IsEnable);
            _resourceUpdateMechanics = new ResourceUpdateMechanics(MaxAmount, Amount, UpdateTime, IsEnable);
        }

        private void OnEnable()
        {
            _gatheringMechanics.OnEnable();
            _enableResourceMechanics.OnEnable();
            _destroyMechanics.OnEnable();
            _resourceUpdateMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringMechanics.OnDisable();
            _enableResourceMechanics.OnDisable();
            _destroyMechanics.OnDisable();
            _resourceUpdateMechanics.OnDisable();
        }
    }
}