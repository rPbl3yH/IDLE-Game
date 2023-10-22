using System.Linq;
using App.Gameplay.Resource;
using UnityEngine;

namespace App.Gameplay
{
    public static class ResourceDetectionUtils
    {
        public static ResourceModel GetClosetAvailableResource(ResourceModel[] resources, Transform point)
        {
            var list = resources.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            return list.FirstOrDefault(model => model.Amount.Value > 0);
        }
    }
}