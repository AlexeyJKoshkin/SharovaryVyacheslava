using UnityEngine;

namespace RoyalAxe 
{
    public abstract class UIViewComponent : MonoBehaviour
    {
        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}