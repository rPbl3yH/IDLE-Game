using System;
using System.Collections;
using UnityEngine;

namespace App.GameEngine
{
    public class ColliderSensor : MonoBehaviour
    {
        public event Action<Collider[]> ColliderUpdated;

        public Collider[] Targets;

        [SerializeField] private Transform _centerPoint;
        [SerializeField] private float _detectionRadius;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private float _delay = 0.1f;

        private void Start()
        {
            Targets = new Collider[3];
            StartCoroutine(ColliderCoroutine());
        }

        // private List<IColliderSensorHandler> _handlers;
        //
        // public void AddHandler(IColliderSensorHandler colliderSensorHandler)
        // {
        //     _handlers.Add(colliderSensorHandler);
        // }
        //
        // public void RemoveHandler(IColliderSensorHandler colliderSensorHandler)
        // {
        //     if (_handlers.Contains(colliderSensorHandler))
        //     {
        //         _handlers.Remove(colliderSensorHandler);
        //     }
        // }

        private IEnumerator ColliderCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                Array.Clear(Targets, 0, Targets.Length);
                var size = Physics.OverlapSphereNonAlloc(_centerPoint.position, _detectionRadius, Targets, _layerMask);

                // foreach (var handler in _handlers)
                // {
                //     handler.OnColliderUpdated(Targets);
                // }
                
                ColliderUpdated?.Invoke(Targets);
            }
        }
    }
}