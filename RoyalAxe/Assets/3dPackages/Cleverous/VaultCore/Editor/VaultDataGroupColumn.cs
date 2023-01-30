// (c) Copyright Cleverous 2022. All rights reserved.

using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Cleverous.VaultDashboard
{
    public abstract class VaultDataGroupColumn : VaultDashboardColumn
    {
        protected static List<IVaultDataGroupButton> AllButtonsCache;
        public abstract VaultDataGroupFoldableButton SelectButtonByFullTypeName(string fullTypeName);
        public abstract VaultDataGroupFoldableButton SelectButtonByDisplayTitle(string title);
        public abstract VaultDataGroupFoldableButton SelectButtonDirectly(VaultDataGroupFoldableButton button);
        public abstract void ScrollTo(VisualElement button);
        public abstract void Filter(string f);
    }
}