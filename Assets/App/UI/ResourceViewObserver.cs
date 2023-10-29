using System;
using System.Collections.Generic;
using App.Gameplay;
using VContainer;
using VContainer.Unity;

namespace App.UI
{
    public class ResourceViewObserver : IDisposable
    {
        private readonly ResourceViewFactory _resourceViewFactory;
        private readonly ResourceStorage _resourceStorage;

        private readonly List<ResourceView> _resourceViews = new();

        [Inject]
        public ResourceViewObserver(ResourceViewFactory resourceViewFactory, ResourceStorage resourceStorage)
        {
            _resourceViewFactory = resourceViewFactory;
            _resourceStorage = resourceStorage;
            _resourceStorage.ResourcesChanged += OnResourcesChanged;
            InitViews();
        }
        
        public void Dispose()
        {
            _resourceStorage.ResourcesChanged -= OnResourcesChanged;
        }
        
        private void InitViews()
        {
            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                var view = _resourceViewFactory.Create();
                _resourceViews.Add(view);    
            }
            
            HideAll();
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> resources)
        {
            HideAll();

            foreach (var resource in resources)
            {
                var text = $"{resource.Key} {resource.Value.Amount}";
                _resourceViews[(int)resource.Key].Show(text);
            }
        }

        private void HideAll()
        {
            foreach (var resourceView in _resourceViews)
            {
                resourceView.Hide();
            }
        }
    }
}
