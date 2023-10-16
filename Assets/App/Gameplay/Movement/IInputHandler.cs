using System;
using UnityEngine;

namespace App.Gameplay.Movement
{
    public interface IInputHandler
    {
        event Action<Vector2> DirectionChanged;
    }
}