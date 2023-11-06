using VContainer;

namespace App.Meta.HintTextService
{
    public class HintTextObserver
    {
        [Inject] 
        private HintTextView _hintTextView;
        
        public void Show(string text)
        {
            _hintTextView.Show(text);    
        }

        public void Hide()
        {
            _hintTextView.Hide();
        }
    }
}