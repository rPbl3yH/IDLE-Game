using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Modules.AudioSystem.UISystem.Content
{
    public class ClickAudioObserver : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private UISoundManager _uiSoundManager;
        
        [Inject]
        public void Construct(UISoundManager uiSoundManager)
        {
            _uiSoundManager = uiSoundManager;
            _button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            _uiSoundManager.PlaySound(UISoundType.Click);   
        }
    }
}