using UnityEngine;

namespace ProjectUI
{
    public interface IMetaSceneUIViewHolder
    {
        void InitMainSceneState();
    }

    public class MainSceneUIView : MonoBehaviour, IMetaSceneUIViewHolder
    {
        [field: SerializeField] public CurrencyController CurrencyController { get; private set; }

        [field: SerializeField] public SwipeController SwipeController { get; private set; }

        public void InitMainSceneState()
        {
            CurrencyController.InitCurrency();
        }
    }
}