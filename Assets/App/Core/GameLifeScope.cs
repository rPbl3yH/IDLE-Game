using App.GameEngine.Input;
using App.GameEngine.Input.Handlers;
using App.Gameplay.Character.Scripts.Model;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameLifeScope : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private CharacterModel _characterModel;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_joystick);
            builder.RegisterInstance(_characterModel);
            builder.RegisterEntryPoint<InputHandler>().As<IInputHandler>();
            builder.RegisterEntryPoint<InputController>();
        }
    }
}