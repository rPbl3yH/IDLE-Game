using App.Core.SaveSystem;
using App.Core.SaveSystem.Mediators.Content;
using App.GameEngine.AI.Spawner;
using App.GameEngine.Input.Handlers;
using App.Gameplay.Building;
using App.Gameplay.Building.Barn;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.Meta;
using App.UI;
using App.UI.UIManager;
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

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureServices(builder);

            ConfigureSaveSystem(builder);
            ConfigureUI(builder);
            
            builder.Register<PlayerResourceViewObserver>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<BuildingSpawner>();
            builder.RegisterComponentInHierarchy<BuildingConstructionSpawner>().AsImplementedInterfaces();
            builder.RegisterInstance(_resourceView);
            builder.RegisterComponentInHierarchy<ResourceStorageModelService>();
            builder.RegisterComponentInHierarchy<PlayerSpawner>();
            builder.RegisterComponentInHierarchy<AICharacterSpawner>();

            builder.RegisterComponentInHierarchy<PlayerArrow>();
            builder.RegisterEntryPoint<PlayerArrowController>(Lifetime.Scoped).AsSelf();

            ConfigureTutorialStep(builder);
            
            builder.RegisterComponentInHierarchy<GameManager>();
        }

        private void ConfigureServices(IContainerBuilder builder)
        {
            builder.Register<PlayerService>(Lifetime.Scoped);
            builder.Register<BarnService>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ResourceService>();
            builder.Register<BuildingConstructionService>(Lifetime.Scoped);
            builder.Register<BarnModelService>(Lifetime.Scoped);
        }

        private void ConfigureUI(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<UIPanelManager>();
            builder.RegisterComponentInHierarchy<Joystick>();
            builder.RegisterEntryPoint<JoystickInputHandler>().As<IInputHandler>();
        }

        private void ConfigureTutorialStep(IContainerBuilder builder)
        {
            builder.Register<TutorialState>(Lifetime.Scoped);
            builder.RegisterEntryPoint<Welcome_TutorialStep>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GatheringWood_TutorialStep>(Lifetime.Scoped);
            builder.RegisterEntryPoint<BuildBarn_TutorialStep>(Lifetime.Scoped);
            builder.RegisterEntryPoint<BuildHouse_TutorialStep>(Lifetime.Scoped);
            builder.RegisterEntryPoint<Congratulation_TutorialStep>(Lifetime.Scoped);
        }

        private void ConfigureSaveSystem(IContainerBuilder builder)
        {
            builder.Register<IGameMediator, ResourceMediator>(Lifetime.Singleton);
            builder.Register<IGameMediator, BuildingConstructionMediator>(Lifetime.Singleton);
            builder.Register<IGameMediator, BarnModelMediator>(Lifetime.Singleton);
            builder.Register<IGameMediator, PlayerMediator>(Lifetime.Singleton);
            builder.Register<IGameMediator, TutorialSaveMediator>(Lifetime.Scoped);
            
            builder.Register<GameSaver>(Lifetime.Singleton);
        }
    }
}

