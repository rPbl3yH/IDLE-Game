using UnityEngine;

namespace App.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class BaseGameView : MonoBehaviour
    {
        [SerializeField] private Transform _worldTarget;
        [SerializeField] private float _offsetY;
        private RectTransform _rectTransform;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_camera != null && _worldTarget != null)
            {
                var offset = new Vector3(0f, _offsetY, 0f);
                _rectTransform.position = Camera.main.WorldToScreenPoint(_worldTarget.position) + offset;
            }
        }

        public void SetTarget(Transform target)
        {
            _worldTarget = target;
        }
    }
}
