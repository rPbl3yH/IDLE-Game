using UnityEngine;

namespace Modules.AudioSystem
{
    public class GameSoundManager : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private GameSoundConfig _config;

        public void PlayAudio(AudioClip audioClip)
        {
            Play(audioClip);
        }
        
        public void PlayAudio(GameSoundType gameSoundType)
        {
            if(_config.Sounds.TryGetValue(gameSoundType, out var sound))
            {
                Play(sound);
            }
        }

        private void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}