using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildObjectMechanics
    {
        private readonly AtomicEvent _built;
        private readonly Transform _spawnPoint;
        private readonly GameObject _prefab;

        public BuildObjectMechanics(AtomicEvent built, GameObject prefab, Transform spawnPoint)
        {
            _built = built;
            _prefab = prefab;
            _spawnPoint = spawnPoint;
        }

        public void OnEnable()
        {
            _built.AddListener(OnBuild);
        }

        public void OnDisable()
        {
            _built.AddListener(OnBuild);
        }

        private void OnBuild()
        {
            Object.Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
        }
    }
}