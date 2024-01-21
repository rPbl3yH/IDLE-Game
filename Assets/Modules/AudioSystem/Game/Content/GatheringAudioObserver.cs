using App.Gameplay;
using App.Gameplay.Resource.Model;
using UnityEngine;
using VContainer;

namespace Modules.AudioSystem.Content
{
    public class GatheringAudioObserver : MonoBehaviour
    {
        [SerializeField] 
        private ResourceModel _resourceModel;
        
        private GameSoundManager _gameSoundManager;

        [Inject]
        public void Construct(GameSoundManager gameSoundManager)
        {
            _gameSoundManager = gameSoundManager;
        }

        private void OnEnable()
        {
            _resourceModel.Gathered.Subscribe(OnGathered);
        }

        private void OnDisable()
        {
            _resourceModel.Gathered.Unsubscribe(OnGathered);
        }

        private void OnGathered(int value)
        {
            switch (_resourceModel.ResourceType)
            {
                case ResourceType.Wood:
                    _gameSoundManager.PlayAudio(GameSoundType.GatheringWood);
                    break;
                case ResourceType.Stone:
                    _gameSoundManager.PlayAudio(GameSoundType.GatheringStone);
                    break;
            }
        }
    }
}