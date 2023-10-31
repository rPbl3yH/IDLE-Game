using System;
using App.Gameplay.Character.Scripts.Model;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private float _speedRate;
        [SerializeField] private Camera _camera;
        
        private PlayerSpawner _playerSpawner;
        private CameraFollowingMechanics _cameraFollowingMechanics;

        [Inject]
        private void Construct(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
        }

        private void PlayerSpawnerOnSpawned(PlayerModel playerModel)
        {
            _cameraFollowingMechanics = new CameraFollowingMechanics(_cameraPoint, playerModel.CharacterModel.Root, _speedRate);
        }

        private void Update()
        {
            _cameraFollowingMechanics?.Update(Time.deltaTime);
        }
    }
}