using UnityEngine;

namespace ProjectUI
{
    public interface IMetaSceneUIViewHolder
    {
        void InitMainSceneState();
    }

    public class MainSceneUIView : MonoBehaviour, IMetaSceneUIViewHolder
    {
        public void InitMainSceneState()
        {
        }
    }
}