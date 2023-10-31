using App.GameEngine.Input;
using App.GameEngine.Input.Handlers;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.UI;
using App.UI.UIManager;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameLifeScope : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private ResourceView _resourceView;
        [SerializeField] private ResourceViewFactory _resourceViewFactory;
        [SerializeField] private ResourceService _resourceService;
        [SerializeField] private ResourceStorageModelService _resourceStorageModelService;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private UISpawnService _uiSpawnService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_joystick);
            builder.RegisterEntryPoint<JoystickInputHandler>().As<IInputHandler>();
            builder.Register<PlayerResourceViewObserver>(Lifetime.Singleton);

            builder.RegisterInstance(_uiSpawnService);
            
            builder.RegisterInstance(_resourceView);
            builder.RegisterInstance(_resourceViewFactory);
            builder.RegisterInstance(_resourceService);
            builder.RegisterInstance(_resourceStorageModelService);
            builder.RegisterInstance(_playerSpawner);
            builder.RegisterEntryPoint<GameManager>();
        }
    }
}