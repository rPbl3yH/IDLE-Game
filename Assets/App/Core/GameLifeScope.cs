using App.Core.SaveSystem;
using App.Core.SaveSystem.Mediators.Content;
using App.GameEngine.AI.Spawner;
using App.GameEngine.Input.Handlers;
using App.Gameplay.Building;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.UI;
using Modules.Tutorial;
using Modules.Tutorial.Content;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameLifeScope : LifetimeScope
    {
        [SerializeField] private ResourceView _resourceView;

        private IContainerBuilder _builder;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            builder.Register<PlayerService>(Lifetime.Scoped);
            builder.Register<BarnService>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ResourceService>();
            builder.Register<BuildingConstructionService>(Lifetime.Scoped);
            builder.Register<BarnModelService>(Lifetime.Scoped);
            
            ConfigureSaveSystem();

            builder.RegisterComponentInHierarchy<Joystick>();
            builder.RegisterEntryPoint<JoystickInputHandler>().As<IInputHandler>();
            builder.Register<PlayerResourceViewObserver>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<BuildingSpawner>();
            builder.RegisterInstance(_resourceView);
            builder.RegisterComponentInHierarchy<ResourceStorageModelService>();
            builder.RegisterComponentInHierarchy<PlayerSpawner>();
            builder.RegisterComponentInHierarchy<AICharacterSpawner>();
            
            ConfigureTutorialStep();
            
            builder.RegisterComponentInHierarchy<GameManager>();
            builder.RegisterBuildCallback(OnRegisterCallback);
        }

        private void OnRegisterCallback(IObjectResolver resolver)
        {
            //resolver.Inject(_saveController);
        }

        private void ConfigureTutorialStep()
        {
            _builder.Register<TutorialState>(Lifetime.Scoped);
            _builder.RegisterEntryPoint<WelcomeTutorialStep>(Lifetime.Scoped);
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

