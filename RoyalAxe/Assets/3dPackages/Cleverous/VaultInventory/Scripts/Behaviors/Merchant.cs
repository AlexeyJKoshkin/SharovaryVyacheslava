// (c) Copyright Cleverous 2023. All rights reserved.

using System;
using Cleverous.VaultSystem;
using Cleverous.NetworkImposter;
using UnityEngine;

#if MIRROR
using Mirror;
#elif FISHNET
using FishNet.Object;
using Command = FishNet.Object.ServerRpcAttribute;
#endif

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// A Merchant / Shop / Trader class.
    /// </summary>
    public class Merchant : InteractableSceneObject
    {
        [Header("Configuration")]
        public string MerchantName;
        public float InteractionRange;
        public AudioClip PurchaseSound;
        public AudioClip DeniedSound;
        [AssetDropdown(typeof(RootItem))]
        public RootItem AcceptedCurrency;
        [AssetDropdown(typeof(RootItem))]
        public RootItem[] ItemsForSale;

        public Action OnOpenMerchant;
        public Action OnCloseMerchant;

        public bool IsOpen { get; protected set; }
        protected IUseInventory Player;

#if MIRROR || !MIRROR && !FISHNET
        protected virtual void Reset()
	    {
#elif FISHNET
        protected override void Reset()
        {
            base.Reset();
#endif
            InteractionRange = 2;
        }
        protected virtual void Awake()
        {
            VaultInventory.OnPlayerSpawn += SetPlayer;
        }
        protected virtual void OnDestroy()
        {
            VaultInventory.OnPlayerSpawn -= SetPlayer;
        }
        public void Update()
        {
            if (!IsOpen || Player == null || Player.MyTransform == null) return;

            if (Vector3.Distance(gameObject.transform.position, Player.MyTransform.position) > InteractionRange)
            {
                // Debug.Log("Too far from Merchant.");
                CloseMerchant();
            }
        }

        /// <summary>
        /// Set the local player that will be interacting with this Merchant.
        /// </summary>
        /// <param name="player"></param>
        protected virtual void SetPlayer(IUseInventory player)
        {
            Player = player;
        }

        /// <summary>
        /// Begin interaction. Open the Merchant UI with this Merchant. Usually called from a trigger callback, some input event, or your UI.
        /// </summary>
        public virtual void OpenMerchant()
        {
            if (IsOpen)
            {
                //Debug.Log("<color=red>Already Open.</color>");
                return;
            }

            if (Player == null || Player.MyTransform == null)
            {
                //Debug.Log($"<color=red>No Player found</color>");
                return;
            }

            float distance = Vector3.Distance(gameObject.transform.position, Player.MyTransform.position);
            if (distance > InteractionRange)
            {
                //Debug.Log($"<color=red>Too far from Merchant. ({distance})</color>");
                CloseMerchant();
                return;
            }

            //Debug.Log("<color=lime>Merchant is open!</color>");
            IsOpen = true;
            OnOpenMerchant?.Invoke();
            if (MerchantUi.Instance != null) MerchantUi.Instance.Open(this);
        }

        /// <summary>
        /// End interaction. Close the MerchantUi instance and flag this Merchant as not being interacted with.
        /// </summary>
        public virtual void CloseMerchant()
        {
            if (MerchantUi.Instance != null) MerchantUi.Instance.Close();
            IsOpen = false;
            OnCloseMerchant?.Invoke();
        }

        /// <summary>
        /// Buy the content at the given index and count.
        /// </summary>
        /// <param name="merchIndex">The index of the merchant's merchandise that the Player is purchasing.</param>
        /// <param name="count">The number of that item that the player is purchasing.</param>
        public virtual void ClientBuy(int merchIndex, int count)
        {
            // ... local client wants to buy item at [index]...
            //Debug.Log($"<color=yellow>Buying {count} of index {merchIndex} ({ItemsForSale[merchIndex].Title})</color>");

            // do some local checks so we don't waste the server's time.
            // it doesn't matter if this is bypassed by exploits, its just a local courtesy to the server.
            int currencyOnHand = Player.Inventory.GetCountOfItem(AcceptedCurrency);
            AudioSource noise = Player.MyTransform.GetComponent<AudioSource>();

            if (ItemsForSale[merchIndex].Value > currencyOnHand)
            {
                // Debug.Log($"<color=red>Client does not have enough currency to buy the item. {ItemsForSale[merchIndex].Value} / {currencyOnHand}</color>");
                noise.clip = DeniedSound;
                noise.Play();
                return;
            }

            // send request to server
            CmdRequestBuyItem(Player.Inventory.NetId(), merchIndex, count);

            // probably will work, so just play some noise.
            noise.clip = PurchaseSound;
            noise.Play();
        }

#if MIRROR
        [Command(requiresAuthority = false)]
#elif FISHNET
        [Command(RequireOwnership = false)]
#endif
        public virtual void CmdRequestBuyItem(uint buyerNetId, int merchIndex, int count)
        {
            IUseInventory buyer = NetworkPipeline.GetNetworkIdentity(buyerNetId, this).GetComponent<IUseInventory>();

            // server-side validation
            int emptyCount = buyer.Inventory.GetEmptySlotCount(); 
            if (emptyCount == 0)
            {
                // Debug.Log($"<color=red>Client has no open slots, and we haven't added merge prediction yet..</color>");
                return;
            }
            if (ItemsForSale[merchIndex].Value > buyer.Inventory.GetCountOfItem(AcceptedCurrency))
            {
                // Debug.Log($"<color=red>Client (buyer: {buyer.MyTransform.name}) does not have enough currency to buy the item.</color>", buyer.MyTransform);
                return;
            }
            float distance = Vector3.Distance(gameObject.transform.position, buyer.MyTransform.position);
            if (distance > InteractionRange)
            {
                // Debug.Log($"<color=red>Too far from Merchant. ({distance:00} / {InteractionRange})</color>");
                CloseMerchant();
                return;
            }

            // execution
            SvrClientBuy(buyer, merchIndex, count);
        }
        [Server]
        public virtual void SvrClientBuy(IUseInventory client, int merchIndex, int count)
        {
            // take money, give item.
            client.Inventory.DoTake(AcceptedCurrency, ItemsForSale[merchIndex].Value * count);
            client.Inventory.DoAdd(new RootItemStack(ItemsForSale[merchIndex], count));
        }

        /// <summary>
        /// Sell the stack of content in the player's inventory at the given index.
        /// </summary>
        /// <param name="clientIndex">The index of the merchant's merchandise that the Player is purchasing.</param>
        public virtual void ClientSell(int clientIndex)
        {
            //Debug.Log($"<color=yellow>Trying to sell index {clientIndex}, it is {m_player.Inventory.Get(clientIndex).StackSize} {m_player.Inventory.Get(clientIndex).Source.Title}</color>", this);
            CmdRequestSellItem(Player.Inventory.NetId(), clientIndex);
        }

#if MIRROR
        [Command(requiresAuthority = false)]
#elif FISHNET
        [Command(RequireOwnership = false)]
#endif
        public virtual void CmdRequestSellItem(uint sellerNetId, int sellerIndex)
        {
            IUseInventory seller = NetworkPipeline.GetNetworkIdentity(sellerNetId, this).GetComponent<IUseInventory>();

            // server-side validation
            int emptyCount = seller.Inventory.GetEmptySlotCount();
            if (emptyCount == 0)
            {
                Debug.Log($"<color=red>Client has no open slots to accept compensation for the sale, and we haven't added merge prediction yet..</color>");
                return;
            }
            float distance = Vector3.Distance(gameObject.transform.position, seller.MyTransform.position);
            if (distance > InteractionRange)
            {
                // Debug.Log($"<color=red>Too far from Merchant. ({distance:00} / {InteractionRange})</color>");
                CloseMerchant();
                return;
            }

            // execution
            SvrClientSell(seller, sellerIndex);
        }
        [Server]
        public virtual void SvrClientSell(IUseInventory client, int clientIndex)
        {
            // take item, give money.
            int saleValue = client.Inventory.Get(clientIndex).GetTotalValue();
            client.Inventory.DoErase(clientIndex);
            client.Inventory.DoAdd(new RootItemStack(AcceptedCurrency, saleValue));
        }

        public override void Interact(IUseInventory interactor = null)
        {
            Player = interactor;
            OpenMerchant();
        }
    }
}