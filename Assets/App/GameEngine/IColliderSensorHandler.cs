using UnityEngine;

namespace App.GameEngine
{
    public interface IColliderSensorHandler
    {
        void OnColliderUpdated(Collider[] colliders);
    }
}