using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    [CreateAssetMenu(menuName = "GameConfigs/Create ResourceStorageConfig", fileName = "ResourceStorageConfig", order = 0)]
    public class ResourceStorageConfig : ScriptableObject
    {
        [ShowInInspector, SerializeReference]
        public List<ResourceData> Resources = new();
    }
}