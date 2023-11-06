using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        
        public void Show(Sprite sprite, string text)
        {
            _text.text = text;
            _icon.sprite = sprite;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}