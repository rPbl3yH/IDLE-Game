using System.Linq;
using App.Gameplay.LevelStorage;
using Atomic;

namespace App.Gameplay
{
    public class UnloadingResourcesMechanics
    {
        private readonly AtomicVariable<LevelStorageModel> _levelStorageModel;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<float> _delay;

        private readonly ResourceStorage _resourceStorage;

        private float _timer;
        
        public UnloadingResourcesMechanics(
            AtomicVariable<LevelStorageModel> levelStorageModel,
            AtomicVariable<bool> canUnloadResources,
            AtomicVariable<float> delay,
            ResourceStorage resourceStorage)
        {
            _levelStorageModel = levelStorageModel;
            _canUnloadResources = canUnloadResources;
            _delay = delay;
            _resourceStorage = resourceStorage;
        }

        public void Update(float deltaTime)
        {
            if (!_canUnloadResources.Value)
            {
                return;
            }
            
            _timer += deltaTime;
            
            if (_timer >= _delay.Value)
            {
                ResetTimer();
                var resources = _resourceStorage.GetAllResources();
                
                if (resources.Count == 0)
                {
                    return;
                }
                
                var resource = resources.First(pair => pair.Value > 0);
                var resourceData = new ResourceData(resource.Key, 1);
                _levelStorageModel.Value.ResourceAdded?.Invoke(resourceData);
                _resourceStorage.TryRemove(resource.Key, 1);
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}