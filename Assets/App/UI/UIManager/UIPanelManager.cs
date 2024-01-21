using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace App.UI.UIManager
{
    public class UIPanelManager : SerializedMonoBehaviour
    {
        [ShowInInspector, OdinSerialize]
        private Dictionary<UIPanelType, BaseUIView> _panels = new();

        public BaseUIView GetPanel(UIPanelType uiPanelType)
        {
            if (_panels.TryGetValue(uiPanelType, out var panel))
            {
                return panel;
            }

            return null;
        }
        
        public void ShowPanel(UIPanelType uiPanelType)
        {
            if (_panels.TryGetValue(uiPanelType, out var panel))
            {
                panel.Show();
            }
        }

        public void HidePanel(UIPanelType uiPanelType)
        {
            if (_panels.TryGetValue(uiPanelType, out var panel))
            {
                panel.Hide();
            }
        }
    }
}