// (c) Copyright Cleverous 2023. All rights reserved.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// Represents a button in the <see cref="UiContextMenu"/>.
    /// </summary>
    public class UiContextMenuButton : MonoBehaviour
    {
        public int Index;
        public Button TargetButton;
        public TMP_Text LabelText;

        public virtual void SetIndex(int i)
        {
            Index = i;
        }
        public virtual void SetText(string t)
        {
            LabelText.text = t;
        }

        // called from the Button UI inspector callback.
        public virtual void Interact()
        {
            UiContextMenu.Instance.ClickedInteractionUiButton(Index);
        }

        public virtual void SetNavigation(Selectable up, Selectable down)
        {
            TargetButton.navigation = new Navigation
            {
                mode = Navigation.Mode.Explicit,
                selectOnDown = down,
                selectOnLeft = TargetButton,
                selectOnRight = TargetButton,
                selectOnUp = up,
                wrapAround = true
            };
        }
    }
}