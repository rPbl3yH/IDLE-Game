using App.Core;
using App.Gameplay.Movement;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeScope : LifetimeScope
{
    [SerializeField] private Joystick _joystick;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_joystick);
        builder.RegisterEntryPoint<InputHandler>().As<IInputHandler>();
        builder.Register<InputController>(Lifetime.Singleton);
    }
}
