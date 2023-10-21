using System;
using UnityEngine;

namespace App.Gameplay.Animation
{
    public class AnimationDispatcher : MonoBehaviour
    {
        public event Action<string> EventRequested;

        public void Invoke(string eventName)
        {
            EventRequested?.Invoke(eventName);
        }
    }
}
