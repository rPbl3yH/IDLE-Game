using System.Collections.Generic;
using System.Linq;
using App.Core.SaveSystem;
using App.Core.SaveSystem.Mediators.Content;
using App.GameEngine.AI.Spawner;
using App.GameEngine.Input;
using App.GameEngine.Input.Handlers;
using App.Gameplay.Building;
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
        [SerializeField] private ResourceService _resourceService;
        [SerializeField] private ResourceStorageModelService _resourceStorageModelService;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private BuildingViewObserver _buildingViewObserver;
        [SerializeField] private BuildingSpawner _buildingSpawner;
        [SerializeField] private AICharacterSpawner _aiCharacterSpawner;

        private IContainerBuilder _builder;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            builder.Register<PlayerService>(Lifetime.Scoped);
            builder.Register<BarnService>(Lifetime.Scoped);
            builder.RegisterInstance(_resourceService);
            builder.Register<BuildingConstructionService>(Lifetime.Scoped);
            builder.Register<BarnModelService>(Lifetime.Scoped);
            ConfigureSaveSystem();

            builder.RegisterInstance(_buildingViewObserver);

            builder.RegisterInstance(_joystick);
            builder.RegisterEntryPoint<JoystickInputHandler>().As<IInputHandler>();
            builder.Register<PlayerResourceViewObserver>(Lifetime.Singleton);

            builder.RegisterInstance(_buildingSpawner);
            builder.RegisterInstance(_resourceView);
            builder.RegisterInstance(_resourceStorageModelService);
            builder.RegisterInstance(_playerSpawner);
            builder.RegisterInstance(_aiCharacterSpawner);
            
            builder.RegisterEntryPoint<GameManager>();

            builder.RegisterBuildCallback(OnRegisterCallback);
        }

        private void OnRegisterCallback(IObjectResolver resolver)
        {
            
            //resolver.Inject(_saveController);
        }

        private void ConfigureSaveSystem()
        {
            _builder.Register<GameRepository>(Lifetime.Singleton);
            _builder.Register<IGameMediator, ResourceMediator>(Lifetime.Singleton);
            _builder.Register<IGameMediator, BuildingConstructionMediator>(Lifetime.Singleton);
            _builder.Register<IGameMediator, BarnModelMediator>(Lifetime.Singleton);
            _builder.Register<IGameMediator, PlayerMediator>(Lifetime.Singleton);
            
            _builder.Register<GameSaver>(Lifetime.Singleton);
        }
    }
}

