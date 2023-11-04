using System;
using System.Collections.Generic;

namespace App.Core
{
    public static class ServiceLocator
    {
        private static List<object> _services = new List<object>();

        public static void Clear()
        {
            _services.Clear();
        }
        
        public static T GetService<T>() {
            foreach (var service in _services) {
                if (service is T result) {
                    return result;
                }
            }

            throw new Exception($"Service {typeof(T).Name} doesn't exist"); 
        }

        public static IEnumerable<T> GetServices<T>()
        {
            var result = new List<T>();
            foreach (var service in _services)
            {
                if (service is T resultService)
                {
                    result.Add(resultService);
                }
            }

            return result;
        }

        public static void AddService(object service)
        {
            _services.Add(service);    
        }

        public static void RemoveService(object service)
        {
            if (_services.Contains(service))
            {
                _services.Remove(service);
            }
        }
    }
}