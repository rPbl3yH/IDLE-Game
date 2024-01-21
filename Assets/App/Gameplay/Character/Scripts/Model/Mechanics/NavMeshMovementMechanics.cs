using Atomic.Elements;
using UnityEngine;
using UnityEngine.AI;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class NavMeshMovementMechanics
    {
        private readonly NavMeshAgent _agent;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _speed;

        public NavMeshMovementMechanics(
            NavMeshAgent agent,
            IAtomicValue<Vector3> moveDirection, 
            IAtomicValue<float> speed)
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