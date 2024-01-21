using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Core
{
    public class GameRepository
    {
        private const string SAVE_KEY = "GameState";
        private Dictionary<string, string> _gameState = new();
        
        public bool TryGetData(string key, out string data)
        {
            return _gameState.TryGetValue(key, out data);
        }

        public string GetData(string data)
        {
            return _gameState[data];
        }

        public void SetData(string key, string data)
        {
            _gameState[key] = data;
        }

        public void SaveState()
        {
            var json = JsonConvert.SerializeObject(_gameState);
            ES3.Save(SAVE_KEY, json);
        }

        public void LoadState()
        {
            if (ES3.KeyExists(SAVE_KEY))
            {
                var localJson = ES3.Load<string>(SAVE_KEY);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(localJson);
            }
        }

        public void ClearState()
        {
            if (ES3.KeyExists(SAVE_KEY))
            {
                ES3.DeleteKey(SAVE_KEY);
            }
        }
    }
}