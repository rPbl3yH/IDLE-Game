using System;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class ApplicationLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ApplicationLoader>(Lifetime.Singleton);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}