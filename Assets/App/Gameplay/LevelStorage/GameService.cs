using System.Collections.Generic;

namespace App.Gameplay.LevelStorage
{
    public abstract class GameService<T>
    {
        private readonly List<T> _services = new();

        public virtual void AddService(T service)
        {
            _services.Add(service);   
        }

        public virtual void RemoveService(T service)
        {
            if (_services.Remove(service))
            {
                _services.Remove(service);
            }
        }

        public virtual IEnumerable<T> GetServices()
        {
            return _services;
        }
    }
}