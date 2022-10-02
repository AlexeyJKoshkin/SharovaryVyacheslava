//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreGamePlayEntity {

    public GemsListenerComponent gemsListener { get { return (GemsListenerComponent)GetComponent(CoreGamePlayComponentsLookup.GemsListener); } }
    public bool hasGemsListener { get { return HasComponent(CoreGamePlayComponentsLookup.GemsListener); } }

    public void AddGemsListener(System.Collections.Generic.List<IGemsListener> newValue) {
        var index = CoreGamePlayComponentsLookup.GemsListener;
        var component = (GemsListenerComponent)CreateComponent(index, typeof(GemsListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGemsListener(System.Collections.Generic.List<IGemsListener> newValue) {
        var index = CoreGamePlayComponentsLookup.GemsListener;
        var component = (GemsListenerComponent)CreateComponent(index, typeof(GemsListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGemsListener() {
        RemoveComponent(CoreGamePlayComponentsLookup.GemsListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CoreGamePlayMatcher {

    static Entitas.IMatcher<CoreGamePlayEntity> _matcherGemsListener;

    public static Entitas.IMatcher<CoreGamePlayEntity> GemsListener {
        get {
            if (_matcherGemsListener == null) {
                var matcher = (Entitas.Matcher<CoreGamePlayEntity>)Entitas.Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.GemsListener);
                matcher.componentNames = CoreGamePlayComponentsLookup.componentNames;
                _matcherGemsListener = matcher;
            }

            return _matcherGemsListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreGamePlayEntity {

    public void AddGemsListener(IGemsListener value) {
        var listeners = hasGemsListener
            ? gemsListener.value
            : new System.Collections.Generic.List<IGemsListener>();
        listeners.Add(value);
        ReplaceGemsListener(listeners);
    }

    public void RemoveGemsListener(IGemsListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gemsListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGemsListener();
        } else {
            ReplaceGemsListener(listeners);
        }
    }
}
