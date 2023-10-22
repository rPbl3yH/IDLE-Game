using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class LevelStorageService : MonoBehaviour
    {
        [SerializeField] private LevelStorageModel _levelStorageModel;

        public LevelStorageModel GetStorage()
        {
            return _levelStorageModel;
        }
    }
}