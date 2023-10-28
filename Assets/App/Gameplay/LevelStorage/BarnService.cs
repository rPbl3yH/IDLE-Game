using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnService : MonoBehaviour
    {
        [SerializeField] private ResourceStorageModel _resourceStorageModel;

        public ResourceStorageModel GetStorage()
        {
            return _resourceStorageModel;
        }
    }
}