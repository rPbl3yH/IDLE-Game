using App.Gameplay;
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
        [SerializeField] private Player _player;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_joystick);
            builder.RegisterInstance(_player);
            builder.RegisterEntryPoint<InputHandler>().As<IInputHandler>();
            builder.RegisterEntryPoint<InputController>();
        }
    }
}