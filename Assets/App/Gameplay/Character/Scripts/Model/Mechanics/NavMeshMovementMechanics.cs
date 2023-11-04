using Modules.Atomic.Values;
using UnityEngine;
using UnityEngine.AI;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class NavMeshMovementMechanics
    {
        private readonly NavMeshAgent _agent;

        private readonly AtomicVariable<Vector3> _moveDirection;

        private readonly AtomicVariable<float> _speed;
        private readonly IAtomicValue<bool> _canMove;

        public NavMeshMovementMechanics(
            NavMeshAgent agent,
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<float> speed)
        {
            _agent = agent;
            _moveDirection = moveDirection;
            _speed = speed;
        }

        public void Update(float deltaTime)
        {
            _agent.Move(_moveDirection.Value * deltaTime * _speed.Value);
        }
    }
}