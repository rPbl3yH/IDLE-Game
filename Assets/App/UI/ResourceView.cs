using TMPro;
using UnityEngine;

namespace App.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void Show(string text)
        {
            _text.text = text;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}