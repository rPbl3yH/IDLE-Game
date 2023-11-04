using Modules.Elementary.Time.Base;
using UnityEngine;
using VContainer;

namespace App.Core
{
    public class SaveController : MonoBehaviour
    {
        [SerializeField] private float _saveDelay;
        
        public GameSaver GameSaver;
        private Timer _timer;
        
        [Inject]
        public void Construct(GameSaver gameSaver)
        {
            print("Game saver inject");
            GameSaver = gameSaver;
            _timer = new Timer(_saveDelay);
            _timer.Play();
            _timer.OnFinished += TimerOnFinished;
        }

        private void TimerOnFinished()
        {
            _timer.ResetTime();
            _timer.Play();
            GameSaver.Save();
        }
    }
}