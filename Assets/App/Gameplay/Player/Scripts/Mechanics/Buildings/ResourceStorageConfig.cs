using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Player
{
    [CreateAssetMenu(menuName = "GameConfigs/Create ResourceStorageConfig", fileName = "ResourceStorageConfig", order = 0)]
    public class ResourceStorageConfig : ScriptableObject
    {
        [ShowInInspector, SerializeReference]
        public Dictionary<ResourceType, int> Resources = new();
    }
}