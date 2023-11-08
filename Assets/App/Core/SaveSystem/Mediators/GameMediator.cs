﻿using Newtonsoft.Json;
using VContainer;

namespace App.Core
{
    public abstract class GameMediator<TData, TGameService> : IGameMediator
    {
        private TGameService _service;

        [Inject]
        public void Construct(TGameService service)
        {
            _service = service;
        }
        
        protected virtual string DataKey
        {
            get { return typeof(TData).Name; }
        }

        void IGameMediator.SetupData(GameRepository repository)
        {
            if (repository.TryGetData(DataKey, out var json))
            {
                var data = JsonConvert.DeserializeObject<TData>(json);
                SetupFromData(_service, data);
            }
            else
            {
                SetupByDefault(_service);
            }
        }

        void IGameMediator.SaveData(GameRepository repository)
        {
            var data = ConvertToData(_service);
            var json = JsonConvert.SerializeObject(data);
            repository.SetData(DataKey, json);
        }

        protected abstract void SetupFromData(TGameService service, TData data);

        protected abstract void SetupByDefault(TGameService service);

        protected abstract TData ConvertToData(TGameService service);
    }
}