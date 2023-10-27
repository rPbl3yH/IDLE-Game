using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnService : MonoBehaviour
    {
        [SerializeField] private BarnModel _barnModel;

        public BarnModel GetStorage()
        {
            return _barnModel;
        }
    }
}