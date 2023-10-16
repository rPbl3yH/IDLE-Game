using System;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class ApplicationLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new SceneLoader());
            builder.Register<ApplicationLoader>(Lifetime.Singleton);
        }
    }
}