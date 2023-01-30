// (c) Copyright Cleverous 2023. All rights reserved.

using System.Collections;
using Cleverous.VaultSystem;
using Cleverous.NetworkImposter;
using UnityEngine;
using Random = UnityEngine.Random;

#if MIRROR
using Mirror;
#elif FISHNET
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Command = FishNet.Object.ServerRpcAttribute;
using ClientRpc = FishNet.Object.ObserversRpcAttribute;
#endif

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Represents a <see cref="RootItem"/> stack in the world space.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class RuntimeItemProxy : InteractableSceneObject
    {
        public static AnimationCurve MoundCurve = new AnimationCurve(new Keyframe(0, 0, -1, 1), new Keyframe(0.5f, 1), new Keyframe(1, 0, -1, 1));
        public RootItemStack Data;
        public GameObject SelectedIndicator;

        public bool AutoPickup;
        public bool SlidingPickup;
        public float LockedForSeconds;
        public LayerMask CanPickMeUp;
        public bool PopSpawn;
        protected float SpawnedAtTime;
        protected GameObject GraphicsObject;
        protected bool IsLockedForPickup;

        [SyncVar] protected Vector3 SpawnPosition;
        [SyncVar] protected Vector3 FinalPosition;
        [SyncVar] protected int ItemDbKey;
        [SyncVar] protected int StackSize;

#if MIRROR || !MIRROR && !FISHNET
        protected virtual void Reset()
        {
#elif FISHNET
        protected override void Reset()
        {
            base.Reset();
#endif
            Data = null;
            AutoPickup = true;
            LockedForSeconds = 1;
            CanPickMeUp = 1;
            PopSpawn = true;
            IsLockedForPickup = false;
        }
        protected virtual void OnEnable()
        {
            InteractionDeselect();
            IsLockedForPickup = false;
            SpawnedAtTime = Time.time;
        }
        protected virtual void OnTriggerEnter(Collider col)
        {
            if (SpawnedAtTime + LockedForSeconds > Time.time) return;
            bool validTouch = ((1 << col.gameObject.layer) & CanPickMeUp) != 0;
            if (!validTouch) return;
            IUseInventory swifferPickerUpper = col.GetComponent<IUseInventory>();
            if (swifferPickerUpper == null) return;

            if (swifferPickerUpper.Inventory.IsLocalPlayer())
            {
                if (!AutoPickup) InteractionSelect(swifferPickerUpper);
            }

            if (this.IsServer())
            {
                if (AutoPickup) RequestPickup(swifferPickerUpper);
            }
        }
        protected virtual void OnTriggerExit(Collider col)
        {
            IUseInventory subject = col.GetComponent<IUseInventory>();
            if (subject != null && subject.Inventory.IsLocalPlayer() && !AutoPickup) InteractionDeselect();
        }

        public override void OnStartClient()
        {
#if FISHNET
            base.OnStartClient();
#endif            
            if (GraphicsObject != null) Destroy(GraphicsObject); // cleanup if necessary.

            // Client has the syncvar by now so they can just pull the data from the Vault.
            RootItem source = (RootItem)Vault.Get(ItemDbKey);

            //Debug.Log($"<color=cyan>Spawned as [{m_itemId}] : ({m_stackSize}) {source.Title}</color>", this);

            Data = new RootItemStack(source, StackSize);
            GraphicsObject = Instantiate(Data.Source.ArtPrefab, transform, false);

            // Server did this already.
            // If not running headless, the server is a client (Host) and reaches this code too, so we'd have to skip it.
            if (PopSpawn && !this.IsServer()) StartCoroutine(SpawnPop());
        }

        /// <summary>
        /// This is called from VaultInventory when spawning the item on the Server. It will setup the syncvars.
        /// </summary>
        /// <param name="sourceItem">The item being spawned</param>
        /// <param name="stackSize">Total number of items in this stack</param>
        public virtual void SvrInitialize(RootItem sourceItem, int stackSize)
        {
            if (!NetworkPipeline.StaticIsServer()) return;

            float mag = Random.value * 5;
            Vector2 rng = Random.insideUnitCircle;

            StackSize = stackSize;
            ItemDbKey = sourceItem.GetDbKey();
            SpawnPosition = transform.position;
            FinalPosition = PopSpawn
                ? SpawnPosition + new Vector3(rng.x * mag, transform.position.y, rng.y * mag)
                : SpawnPosition;

            // Need to do this here for headless, so it's here as well as the client method (bypassed there if server).
            if (PopSpawn) StartCoroutine(SpawnPop());
        }

        /// <summary>
        /// Try to pick up the item.
        /// </summary>
        /// <param name="requestor">The Inventory interface trying to claim the item.</param>
        public virtual void RequestPickup(IUseInventory requestor)
        {
            if (requestor == null || IsLockedForPickup) return;

            // Server asked? Move to validation.
            if (requestor.Inventory.IsServer())
            {
                int remainder = PerformPickup(requestor);
                if (remainder == 0 && SlidingPickup)
                {
                    // Slide Local.
                    StopAllCoroutines();
                    StartCoroutine(StartSlidingPickup(requestor));

                    int behaviorIndex = 0;
                    NetworkBehaviour[] behaviours = requestor.Inventory.GetNetworkBehaviours();
                    for (int i = 0; i < behaviours.Length; i++)
                    {
                        if (behaviours[i] == requestor.Inventory) behaviorIndex = i;
                    }

                    // RPC Slide to other clients.
                    RpcSlideStart(requestor.Inventory.NetId(), behaviorIndex);
                }
                return;
            }

            // Client asked? Tell server.
            bool fits = requestor.Inventory.StackCanFit(Data);
            if (requestor.Inventory.IsLocalPlayer() && !this.IsServer() && fits)
            {
                int behaviorIndex = 0;
                NetworkBehaviour[] behaviours = requestor.Inventory.GetNetworkBehaviours();
                for (int i = 0; i < behaviours.Length; i++)
                {
                    if (behaviours[i] == requestor.Inventory) behaviorIndex = i;
                }
                CmdRequestPickup(requestor.Inventory.NetId(), behaviorIndex);
            }
        }

        /// <summary>
        /// Slides the item object through the world toward the transform of the inventory owner.
        /// </summary>
        /// <param name="target">The inventory target</param>
        protected virtual IEnumerator StartSlidingPickup(IUseInventory target)
        {
            InteractionDeselect();
            IsLockedForPickup = true;

            float time = 0;
            while (Vector3.Distance(transform.position, target.MyTransform.position) > 0.1f)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(FinalPosition, target.MyTransform.position, time * 10);
                yield return null;
            }

            DeSpawn();
        }

#if MIRROR
        [Command(requiresAuthority = false)]
#elif FISHNET
        [Command(RequireOwnership = false)]
#endif
        public virtual void CmdRequestPickup(uint networkId, int behaviorIndex)
        {
            NetworkBehaviour nb = NetworkPipeline.GetNetworkBehaviour(networkId, behaviorIndex);
            IUseInventory requestor = nb.GetComponent<IUseInventory>();

            // TODO
            // Need to validate on server side that the client is within range, but this is done via
            // the InteractableSelector.cs class now, which the client has control of sending requests.
            // For now, this could lead to invalid requests if there was code injection or packet faking.

            // Add
            int remainder = PerformPickup(requestor);

            // Visualize
            if (SlidingPickup && remainder == 0)
            {
                // stop local events.
                StopAllCoroutines();

                // start locally.
                StopAllCoroutines();
                StartCoroutine(StartSlidingPickup(requestor));

                // tell everyone else.
                RpcSlideStart(networkId, behaviorIndex);
            }
        }

        [ClientRpc]
        public virtual void RpcSlideStart(uint networkId, int behaviorIndex)
        {
            NetworkBehaviour target = NetworkPipeline.GetNetworkBehaviour(networkId, behaviorIndex, this);
            IUseInventory requestor = target.GetComponent<IUseInventory>();

            StopAllCoroutines();
            StartCoroutine(StartSlidingPickup(requestor));
        }

        /// <summary>
        /// <para>Returns 0 if there was no remainder left on the object.</para>
        /// <para>Returns the remainder that could not be picked up.</para>
        /// </summary>
        [Server]
        protected virtual int PerformPickup(IUseInventory target)
        {
            int remainder = VaultInventory.TryGiveItem(target.Inventory, Data);
            if (remainder <= 0 && !SlidingPickup)
            {
                DeSpawn();
                return 0;
            }

            Data.StackSize = remainder;
            return remainder;
        }

        /// <summary>
        /// <para>Modifies the Transform of the object locally to create a "pop" movement from the origin position to the origin+offset position.</para>
        /// <para>We don't need NetworkTransform components to sync the transform for all of the items because the SyncVars sync it one time.</para>
        /// </summary>
        protected virtual IEnumerator SpawnPop()
        {
            const float totalTime = 0.5f;
            float time = 0;
            while (time < totalTime)
            {
                time += Time.deltaTime;
                Vector3 lerp = Vector3.Lerp(SpawnPosition, FinalPosition, time / totalTime);
                lerp.y = SpawnPosition.y + 2 * MoundCurve.Evaluate(time / totalTime);
                transform.position = lerp;
                yield return null;
            }

            transform.position = FinalPosition;
        }


        /// <summary>
        /// Destroy or pool the object.
        /// </summary>
        public virtual void DeSpawn()
        {
            StopAllCoroutines();
            InteractionDeselect();
            if (this.IsServer()) Destroy(gameObject);
        }

        /// <summary>
        /// <para>Select the object. This is not Interact().</para>
        /// <para>The Select() method can manage a singleton instance of a selected object for you. Useful for world items you want to interact with.</para>
        /// </summary>
        public override void InteractionSelect(IUseInventory interactor)
        {
            base.InteractionSelect(interactor);
            if (AutoPickup) RequestPickup(interactor);
            if (SelectedIndicator != null) SelectedIndicator.SetActive(true);
        }

        /// <summary>
        /// <para>Deselect the object.</para>
        /// </summary>
        public override void InteractionDeselect(IUseInventory interactor = null)
        {
            base.InteractionDeselect(interactor);
            if (SelectedIndicator != null) SelectedIndicator.SetActive(false);
        }

        /// <summary>
        /// Interact with the item proxy, which by default will try to collect it.
        /// </summary>
        /// <param name="interactor">Who is collecting this object?</param>
        public override void Interact(IUseInventory interactor = null)
        {
            RequestPickup(interactor);
        }
    }
}