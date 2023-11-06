using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class ApplicationLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            
            ConfigureSaveSystem(builder);

            builder.Register<ILoadingTask, LoadingTask_LoadPlayerData>(Lifetime.Scoped);
            builder.Register<ILoadingTask, LoadingTask_LoadGameScene>(Lifetime.Scoped);
            builder.Register<LoadingPipeline>(Lifetime.Scoped);
        }
        
        private void ConfigureSaveSystem(IContainerBuilder builder)
        {
            builder.Register<GameRepository>(Lifetime.Singleton);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}