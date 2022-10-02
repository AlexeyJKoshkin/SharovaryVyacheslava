/*#if UNITY_EDITOR

namespace Utils.Odin {
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using System;
    using System.Collections.Generic;
    using UnityEngine.AddressableAssets;    

    public class OdinSerializedAssetReferenceFix : OdinAttributeProcessor<AssetReference>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new DrawWithUnityAttribute());
        }
    }
    
    public class OdinSerializedAssetReferenceFix2 : OdinAttributeProcessor<AssetReferenceSprite>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new DrawWithUnityAttribute());
        }
    }

}
#endif*/