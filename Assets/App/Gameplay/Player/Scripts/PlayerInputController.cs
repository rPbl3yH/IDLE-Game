using App.GameEngine.Input.Handlers;
using App.Gameplay.Character.Scripts.Model;
using Atomic.Elements;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private IInputHandler _inputHandler;
        private PlayerSpawner _playerSpawner;
        private IAtomicVariable<Vector3> _moveDirection;

        private PlayerModel _playerModel;

        [Inject]
        private void Construct(IInputHandler inputHandler, PlayerSpawner playerSpawner)
        {
            _inputHandler = inputHandler;
            _playerSpawner = playerSpawner;
            
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
            _inputHandler.DirectionChanged += InputHandlerOnDirectionChanged;
        }

        private void InputHandlerOnDirectionChanged(Vector2 moveDirection)
        {
            if (_playerModel == null)
            {
                return;
            }

            var direction = new Vector3(moveDirection.x, 0f, moveDirection.y);
            _moveDirection.Value = direction;
        }

        private void PlayerSpawnerOnSpawned(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _moveDirection = _playerModel.CharacterModel.Get<IAtomicVariable<Vector3>>("MoveDirection");
        }
    }
}