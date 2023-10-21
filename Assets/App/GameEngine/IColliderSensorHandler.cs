using UnityEngine;

namespace App.Gameplay
{
    public interface IColliderSensorHandler
    {
        void OnColliderUpdated(Collider[] colliders);
    }
}