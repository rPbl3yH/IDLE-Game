using System;
using UnityEngine;

namespace App.GameEngine.Input.Handlers
{
    public interface IInputHandler
    {
        event Action<Vector3> DirectionChanged;
    }
}