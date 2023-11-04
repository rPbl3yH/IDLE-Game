using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public abstract class ResourceStorageModel : BuildingModel
    {
        public ResourceStorage ResourceStorage;
        public Transform UnloadingPoint;
    }
}