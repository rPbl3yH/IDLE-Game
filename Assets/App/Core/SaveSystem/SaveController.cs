using UnityEngine;
using VContainer;

namespace App.Core.SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        public GameSaver GameSaver;

        [Inject]
        public void Construct(GameSaver gameSaver)
        {
            print("Game saver inject");
            GameSaver = gameSaver;
        }
    }
}