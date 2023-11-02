using System;
using UnityEngine;

namespace App.Gameplay.Following
{
    public class FollowComponent : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _target;

        private void Update()
        {
            _target.position = _root.position;
        }
    }
}