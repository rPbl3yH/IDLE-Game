using Modules.Elementary.Time.Base;
using UnityEngine;
using VContainer;

namespace App.Core
{
    public class SaveController : MonoBehaviour
    {
        [SerializeField] private float _saveDelay;
        [SerializeField] private bool _autoSave;
        
        public GameSaver GameSaver;
        private Timer _timer;
        
        [Inject]
        public void Construct(GameSaver gameSaver)
        {
            print("Game saver inject");
            GameSaver = gameSaver;
            
            if (!_autoSave)
            {
                return;
            }
            
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