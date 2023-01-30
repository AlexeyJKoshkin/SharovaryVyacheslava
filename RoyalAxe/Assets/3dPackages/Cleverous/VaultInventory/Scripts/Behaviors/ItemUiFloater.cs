// (c) Copyright Cleverous 2023. All rights reserved.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cleverous.VaultInventory
{
    /// <summary>
    /// The 'floating' thing that follows the mouse cursor when you are dragging an item around in the Canvas.
    /// </summary>
    public class ItemUiFloater : MonoBehaviour
    {
        public TMP_Text StackSizeText;
        public Image MyImage;

        public virtual void Set(Sprite sprite, string text)
        {
            MyImage.sprite = sprite;
            StackSizeText.text = text;
        }
    }
}