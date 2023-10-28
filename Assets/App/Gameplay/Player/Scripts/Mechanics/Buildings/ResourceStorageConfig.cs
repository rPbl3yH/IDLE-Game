using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Player
{
    [CreateAssetMenu(menuName = "GameConfigs/Create ResourceStorageConfig", fileName = "ResourceStorageConfig", order = 0)]
    public class ResourceStorageConfig : ScriptableObject
    {
        [ShowInInspector, SerializeReference]
        public List<ResourceData> Resources = new();
    }
}