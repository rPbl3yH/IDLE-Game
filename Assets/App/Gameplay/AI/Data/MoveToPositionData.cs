﻿using System;
using UnityEngine;

namespace App.Gameplay.AI.Model
{
    [Serializable]
    public class MoveToPositionData
    {
        public bool IsEnable;
        public Vector3 TargetPosition;
        public float StoppingDistance;
        public bool IsPositionReached;
    }
}