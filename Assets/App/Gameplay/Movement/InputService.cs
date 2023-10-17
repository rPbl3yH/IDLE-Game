using App.Gameplay.Movement;
using UnityEngine;
using VContainer;

namespace App.Core
{
    public class InputService : MonoBehaviour
    {
        [Inject] private InputController _inputController;
    }
}