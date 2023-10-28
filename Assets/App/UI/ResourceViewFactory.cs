using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace App.UI
{
    public class ResourceViewFactory : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        [Inject] private ResourceView _resourceViewPrefab;

        [Button]
        public ResourceView Create()
        {
            return Instantiate(_resourceViewPrefab, _parent);
        }
    }
}