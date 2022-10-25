using UnityEngine;
using UnityEngine.UI;

namespace RoyalAxe 
{
    public class LoseWindowView : UIViewComponent
    {
        [field:SerializeField]
        public Button LoadMetaBtn { get; private set; }
        [field:SerializeField]
        public Button RetryBtn { get; private set; }
        [field:SerializeField]
        public Button LoadLastPointBtn { get; private set; }
    }
}