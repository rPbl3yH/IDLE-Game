using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.GameEngine.AI.Spawner
{
    public class AICharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private CharacterAIModel _characterAIModel;
        
        [Inject] private IObjectResolver _objectResolver;

        public void Spawn(Transform spawnPoint)
        {
            _objectResolver.Instantiate(_characterAIModel, spawnPoint.position, spawnPoint.rotation, _parent);
        }
    }
}