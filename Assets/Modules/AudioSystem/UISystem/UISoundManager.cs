using UnityEngine;

namespace Modules.AudioSystem.UISystem
{
    public class UISoundManager : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField] 
        private UISoundConfig _config;
        
        public void PlaySound(UISoundType uiSoundType)
        {
            if (_config.Sounds.TryGetValue(uiSoundType, out var sound))
            {
                _audioSource.clip = sound;
                _audioSource.Play();
            }
        }
    }
}