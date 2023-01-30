// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Will facilitate interaction between ui components and the <see cref="Merchant"/>.
    /// </summary>
    public class MerchantUi : MonoBehaviour
    {
        public static MerchantUi Instance;
        public static bool IsOpen { get; protected set; }
        public static Action OnOpened;
        public static Action OnClosed;

        [Header("References")]
        [Tooltip("The GameObject that contains the UI elements that control this Merchant. Should be either this gameObject, or one higher in the hierarchy - a parent 'wrapper' object.")]
        public GameObject Container;
        public RectTransform ContentContainer;
        public TMP_Text NameText;
        public GameObject ListItemPrefab;

        private Merchant m_merchant;
        private List<GameObject> m_uiItems;

        public void Awake()
        {
            if (Container == null) Container = gameObject;
            m_uiItems = new List<GameObject>();
            Instance = this;
            Container.SetActive(false);
        }

        public virtual void Open(Merchant merchant)
        {
            IsOpen = true;
            m_merchant = merchant;
            if (NameText != null) NameText.text = merchant.MerchantName;
            Container.SetActive(true);

            for (int i = 0; i < merchant.ItemsForSale.Length; i++)
            {
                GameObject go = Instantiate(ListItemPrefab, ContentContainer);
                m_uiItems.Add(go);

                MerchantListItem row = go.GetComponent<MerchantListItem>();
                row.Setup(m_merchant, this, i);
            }

            OnOpened?.Invoke();
        }
        public virtual void Close()
        {
            IsOpen = false;
            m_merchant = null;
            foreach (GameObject x in m_uiItems)
            {
                Destroy(x);
            }
            Container.SetActive(false);

            OnClosed?.Invoke();
        }

        public virtual void ClientBuy(int index, int count)
        {
            if (m_merchant == null) return;
            m_merchant.ClientBuy(index, count);
        }
        public virtual void ClientSell(int index)
        {
            m_merchant.ClientSell(index);
        }

        public virtual void OnDisable()
        {
            IsOpen = false;
        }
    }
}