using System;
using System.Collections.Generic;
using App.Gameplay;

namespace App.Core.SaveSystem.Mediators.Content
{
    [Serializable]
    public class BarnModelData
    {
        public List<Dictionary<ResourceType, ResourceValue>> Resources;

        public BarnModelData()
        {
            Resources = new List<Dictionary<ResourceType, ResourceValue>>();
        }
    }
}