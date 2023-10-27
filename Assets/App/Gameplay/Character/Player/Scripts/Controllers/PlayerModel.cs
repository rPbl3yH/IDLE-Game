using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private BarnService _barnService;
        
        private BarnDetectMechanics _barnDetectMechanics;
        private ResourceDetectMechanics _resourceDetectMechanics;
        private UnloadResourceMechanics _unloadResourceMechanics;
        
        private void Awake()
        {
            _barnDetectMechanics = new BarnDetectMechanics(_characterModel, _barnService);
            _resourceDetectMechanics = new ResourceDetectMechanics(_characterModel);
            _unloadResourceMechanics = new UnloadResourceMechanics(_characterModel, _barnService);
        }

        private void Update()
        {
            _barnDetectMechanics.Update();
            _resourceDetectMechanics.Update();
            _unloadResourceMechanics.Update();
        }
    }
}