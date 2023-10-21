using App.Gameplay.Character;
using App.Gameplay.Movement;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameLifeScope : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerModel _playerModel;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_joystick);
            builder.RegisterInstance(_playerModel);
            builder.RegisterEntryPoint<InputHandler>().As<IInputHandler>();
            builder.RegisterEntryPoint<InputController>();
        }
    }
}