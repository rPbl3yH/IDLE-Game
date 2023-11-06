using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.AudioSystem
{
    [CreateAssetMenu(menuName = "Create GameSoundConfig", fileName = "GameSoundConfig", order = 0)]
    public class GameSoundConfig : SerializedScriptableObject
    {
        [OdinSerialize] 
        public Dictionary<GameSoundType, AudioClip> Sounds = new();
    }
}