using App.Gameplay;
using UnityEngine;
using VContainer;

namespace App.UI
{
    public class ResourceIconService
    {
        [Inject]
        private ResourceIconConfig _config;

        public Sprite GetIcon(ResourceType resourceType)
        {
            if (_config.Sprites.TryGetValue(resourceType, out var icon))
            {
                return icon;
            }

            return null;
        }
    }
}