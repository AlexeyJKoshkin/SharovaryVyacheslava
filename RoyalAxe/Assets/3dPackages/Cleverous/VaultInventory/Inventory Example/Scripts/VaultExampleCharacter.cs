// (c) Copyright Cleverous 2023. All rights reserved.

using Cleverous.VaultSystem;
using Cleverous.NetworkImposter;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

#if MIRROR
using Mirror;
#elif FISHNET
using FishNet;
using FishNet.Object;
using Command = FishNet.Object.ServerRpcAttribute;
using NetworkIdentity = FishNet.Object.NetworkObject;
using FishNet.Object.Synchronizing;
#endif

namespace Cleverous.VaultInventory.Example
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(NetworkIdentity))]
    public class VaultExampleCharacter : NetworkBehaviour, IUseInventory
    {
        [AssetDropdown(typeof(LootTable))]
        public LootTable StartingItems;
        public CharacterController Controller;
        public Animator Animator;
        public MeshRenderer PlayerNode;
        public ParticleSystem WalkingTrail;
        [SyncVar] private Color m_playerColor;
        [SyncVar] private bool m_isMoving;

        // we satisfy the interface here and provide a serialized backing field below.
        public Inventory Inventory
        {
            get => m_inventory;
            set => m_inventory = value;
        }
        [SerializeField] private Inventory m_inventory;

        public Transform MyTransform => transform;
        private Vector3 m_inputLast;

        public override void OnStartServer()
        {
#if FISHNET
            base.OnStartServer();
#endif
            // The server should initialize the Colors and set the SyncVar value for clients.
            Random rng = new Random();
            m_playerColor = new Color((float)rng.NextDouble(), (float)rng.NextDouble(), (float)rng.NextDouble(), 1);
        }
        public override void OnStartClient()
        {
#if FISHNET
            base.OnStartClient();
            if (!this.IsOwner) Controller.enabled = false;
#endif
            Inventory.Initialize(this, true);

            // If this is happening on the server, lets give our character some starting items using a LootTable!
            if (this.IsServer())
            {
                for (int i = 0; i < StartingItems.Items.Length; i++)
                {
                    if (StartingItems.Items[i] == null) continue;
                    Inventory.DoAdd(new RootItemStack(StartingItems.Items[i], StartingItems.Amounts[i]));
                }
            }

            if (this.IsLocalPlayer())
            {
                // Tell Vault and the UI that we are here and ready.
                VaultInventory.OnPlayerSpawn.Invoke(this);
            }

            PlayerNode.material.color = m_playerColor;

            if (this.IsLocalPlayer()) Inventory.CmdRefreshAllFromServer();
        }

        private void Update()
        {
            // If this object isn't the local player then we can't control it.
            Animator.SetBool("IsMoving", m_isMoving);

            // The example has a neat little walking trail of particles, and we manage it here.
            if (WalkingTrail != null)
            {
                switch (m_isMoving)
                {
                    case true when !WalkingTrail.isPlaying: WalkingTrail.Play(true); break;
                    case false when WalkingTrail.isPlaying: WalkingTrail.Stop(true, ParticleSystemStopBehavior.StopEmitting); break;
                }
            }

            // We don't do the following unless we are the local player.
            if (!this.IsLocalPlayer()) return;

            // Lets watch for ui toggles
            bool togglePanels = VaultInventory.GetPressedToggleInventory();
            if (togglePanels) VaultExampleUiProxy.Instance.Toggle();

            Movement();
        }

        private void Movement()
        {
            bool moving;

            // Here we use our example ui proxy to tell us if the panel is open or not.
            // We don't want to allow the character to interact with the non-ui world with their inputs while menus are open.
            if (!VaultExampleUiProxy.Instance.CharacterPanelIsShown)
            {
                // Lets collect our movement inputs from the player
                bool a = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed;
                bool d = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed;
                bool w = Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed;
                bool s = Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed;
                Vector2 moveInput = Gamepad.current == null ? Vector3.zero : Gamepad.current.leftStick.ReadValue();

                if (a) moveInput.x -= 1;
                if (d) moveInput.x += 1;
                if (w) moveInput.y += 1;
                if (s) moveInput.y -= 1;

                // Do movement things! This is super primitive as an example.
                moveInput.Normalize();
                moving = moveInput.magnitude > 0;
                Vector3 inputRelative = new Vector3(moveInput.x, 0, moveInput.y);
                Quaternion look = Quaternion.LookRotation(moving ? inputRelative : transform.forward, Vector3.up);
                transform.rotation = look;
                m_inputLast = inputRelative;
            }
            else
            {
                moving = false;
                m_inputLast = Vector3.zero;

                if (InventoryUi.ClickedItem != null && VaultInventory.GetPressedContext())
                {
                    UiContextMenu.Instance.ShowContextMenu(InventoryUi.ClickedItem);
                }
            }

            RpcMoving(moving);
            m_isMoving = moving;

            if (this.IsLocalPlayer()) Controller.Move(m_inputLast * 5 * Time.deltaTime);
        }

        [Command]
        private void RpcMoving(bool m)
        {
            m_isMoving = m;
        }
    }
}