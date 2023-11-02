using System;
using App.Gameplay.LevelStorage;
using App.UI.UIManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Building
{
    [Serializable]
    public class BuildingSpawnPointDictionary : UnitySerializedDictionary<Transform, BuildingModel>{}
    

    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField]
        private BuildingSpawnPointDictionary _buildings = new();

        [Inject] 
        private IObjectResolver _objectResolver;

        [Inject] 
        private ResourceStorageModelService _resourceStorageModelService;
        
        private void Start()
        {
            foreach (var pair in _buildings)
            {
                var buildingModel = _objectResolver.Instantiate(pair.Value, pair.Key);
                _resourceStorageModelService.AddStorage(buildingModel);
            }
        }
    }
}
