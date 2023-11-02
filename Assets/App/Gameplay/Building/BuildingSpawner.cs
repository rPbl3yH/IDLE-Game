    using App.Gameplay.LevelStorage;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [Inject] private IObjectResolver _objectResolver;
        
        
        public LevelStorage.Building Spawn(LevelStorage.Building buildingModel, Transform point)
        {
            return _objectResolver.Instantiate(buildingModel, point.position, point.rotation, _parent);
        }
    }
}