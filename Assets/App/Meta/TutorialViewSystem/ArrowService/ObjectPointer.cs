using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Meta
{
    public class ObjectPointer : MonoBehaviour
    {
        [SerializeField] private Transform _pointer;
        [SerializeField] private Vector3 _jumpPosition = new Vector3(0f, 3f, 0f);
        [SerializeField] private float _force;
        [SerializeField] private float _duration = 0.4f;

        private Tween _tween;

        [Button]
        private void Start()
        {
            _tween?.Kill();
            _tween = _pointer.DOLocalJump(_jumpPosition, _force, 1, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_pointer.gameObject);
        }
    }
}