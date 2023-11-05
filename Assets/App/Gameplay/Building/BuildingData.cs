using System;
using App.Gameplay.LevelStorage;

namespace App.Gameplay.Building
{
    [Serializable]
    public class BuildingData
    {
        public BuildingModel BuildingModel;
        public ResourceStorageConfig BuildConfig;
    }
}