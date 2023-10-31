using UnityEngine;

namespace App.UI.UIManager
{
    public class UISpawnService : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _buildingUiParent;

        public T Spawn<T>(T prefab) where T : Object
        {
            return Instantiate(prefab, _spawnPoint);
        }
        
        public T SpawnBuildingView<T>(T prefab) where T : Object
        {
            return Instantiate(prefab, _buildingUiParent);
        }
    }
}