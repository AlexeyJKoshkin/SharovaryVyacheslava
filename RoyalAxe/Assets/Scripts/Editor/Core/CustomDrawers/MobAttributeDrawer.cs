using System.Linq;
using GameKit.Editor;
using RoyalAxe.Configs;
using RoyalAxe.Utility;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace RoyalAxe.Editor.Core.CustomDrawers
{
    public class MobAttributeDrawer : OdinAttributeDrawer<MobIdAttribute, string>
    {
        private static EnemyConfigProvider _configProvider;
        
        PopUpSelector<string> _mobSelector = new PopUpSelector<string>();

        static MobAttributeDrawer()
        {
            _configProvider = EditorUtils.FindAsset<EnemyConfigProvider>();
            _configProvider.Reload();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _mobSelector.InitValues(_configProvider.Collection.Select(o=> o.UniqueID));
            _mobSelector.SetCurrent(this.ValueEntry.SmartValue);
            _mobSelector.OnSelectNewItemCallback = s => this.ValueEntry.SmartValue = s;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            _mobSelector.DoSelectGUI();
        }
    }
}