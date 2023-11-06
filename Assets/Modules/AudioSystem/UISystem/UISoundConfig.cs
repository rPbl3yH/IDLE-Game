using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.AudioSystem.UISystem
{
    [CreateAssetMenu(menuName = "Create UISoundConfig", fileName = "UISoundConfig", order = 0)]
    public class UISoundConfig : SerializedScriptableObject
    {
        [OdinSerialize] 
        public Dictionary<UISoundType, AudioClip> Sounds = new();
    }
}