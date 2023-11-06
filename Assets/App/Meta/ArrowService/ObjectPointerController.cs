using UnityEngine;
using VContainer;

namespace App.Meta
{
    public class ObjectPointerController
    {
        [Inject] 
        private ObjectPointer _objectPointer;
        
        public void SetTarget(Transform root)
        {
            if (root == null)
            {
                _objectPointer.gameObject.SetActive(false);
                return;
            }
            
            _objectPointer.gameObject.SetActive(true);
            _objectPointer.transform.position = root.position;
        }
    }
}