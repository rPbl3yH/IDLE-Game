using System.Collections.Generic;
using App.Gameplay;

namespace App.Core.SaveSystem.Mediators.Content
{
    public class BuildingConstructionData
    {
        public readonly List<Dictionary<ResourceType, ResourceValue>> Resources;

        public BuildingConstructionData()
        {
            Resources = new List<Dictionary<ResourceType, ResourceValue>>();
        }
    }
}