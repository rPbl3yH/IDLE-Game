using System.Collections.Generic;

namespace App.Gameplay.LevelStorage
{
    public abstract class GameService<T>
    {
        protected readonly List<T> Services = new();

        public virtual void AddService(T service)
        {
            Services.Add(service);   
        }

        public virtual void RemoveService(T service)
        {
            if (Services.Remove(service))
            {
                Services.Remove(service);
            }
        }

        public virtual IEnumerable<T> GetServices()
        {
            return Services;
        }
    }
}