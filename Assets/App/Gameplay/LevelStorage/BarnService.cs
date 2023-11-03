namespace App.Gameplay.LevelStorage
{
    public class BarnService
    {
        private ResourceStorageModel _resourceStorageModel;

        public void RegisterBarn(ResourceStorageModel resourceStorageModel)
        {
            _resourceStorageModel = resourceStorageModel;
        }

        public ResourceStorageModel GetStorage()
        {
            return _resourceStorageModel;
        }
    }
}