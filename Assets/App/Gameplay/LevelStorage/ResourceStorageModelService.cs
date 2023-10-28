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
            var closetModels =
                _resourceStorageModels.OrderBy(model => Vector3.Distance(model.UnloadingPoint.position, root.position));

            return closetModels.FirstOrDefault(model => model.isActiveAndEnabled);
        }
    }
}