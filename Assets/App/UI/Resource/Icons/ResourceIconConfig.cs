using System.Collections.Generic;
using App.Gameplay;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace App.UI
{
    [CreateAssetMenu(menuName = "Create ResourceIconConfig", fileName = "ResourceIconConfig", order = 0)]
    public class ResourceIconConfig : SerializedScriptableObject
    {
        [OdinSerialize] 
        public Dictionary<ResourceType, Sprite> Sprites = new();
    }
}