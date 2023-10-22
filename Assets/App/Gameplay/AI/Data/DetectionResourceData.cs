using System;
using App.Gameplay.Resource;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    [Serializable]
    public class DetectionResourceData
    {
        public bool IsEnable;
        public ResourceService ResourceService;
        public Transform Root;
    }
}