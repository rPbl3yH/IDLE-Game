using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public abstract class ResourceStorageModel : Building
    {
        public ResourceStorage ResourceStorage;
        public Transform UnloadingPoint;
    }
}