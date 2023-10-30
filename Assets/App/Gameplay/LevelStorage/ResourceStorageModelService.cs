using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class ResourceStorageModelService : MonoBehaviour
    {
        [SerializeField] private List<ResourceStorageModel> _resourceStorageModels;

        public ResourceStorageModel GetClosetModel(Transform root)
        {
            if (_resourceStorageModels.Count == 0)
            {
                return null;
            }
            
            var closetModels =
                _resourceStorageModels.OrderBy(model => Vector3.Distance(model.UnloadingPoint.position, root.position));

            var model = closetModels.FirstOrDefault(model => model.isActiveAndEnabled);
            return model;
        }

        public void AddStorage(ResourceStorageModel resourceStorageModel)
        {
            _resourceStorageModels.Add(resourceStorageModel);
        }

        public void RemoveStorage(ResourceStorageModel resourceStorageModel)
        {
            if (_resourceStorageModels.Contains(resourceStorageModel))
            {
                _resourceStorageModels.Remove(resourceStorageModel);
            }
        }
    }
}