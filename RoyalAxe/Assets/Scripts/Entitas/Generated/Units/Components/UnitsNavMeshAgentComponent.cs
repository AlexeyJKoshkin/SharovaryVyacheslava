//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.NavMeshAgentComponent navMeshAgent { get { return (RoyalAxe.GameEntitas.NavMeshAgentComponent)GetComponent(UnitsComponentsLookup.NavMeshAgent); } }
    public bool hasNavMeshAgent { get { return HasComponent(UnitsComponentsLookup.NavMeshAgent); } }

    public void AddNavMeshAgent(float newSpeed, UnityEngine.AI.NavMeshAgent newView) {
        var index = UnitsComponentsLookup.NavMeshAgent;
        var component = (RoyalAxe.GameEntitas.NavMeshAgentComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.NavMeshAgentComponent));
        component.NavMeshAgent = newView;
        component.Speed = newSpeed;
        AddComponent(index, component);
    }

    public void ReplaceNavMeshAgent(float newSpeed, UnityEngine.AI.NavMeshAgent newView) {
        var index = UnitsComponentsLookup.NavMeshAgent;
        var component = (RoyalAxe.GameEntitas.NavMeshAgentComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.NavMeshAgentComponent));
        component.NavMeshAgent = newView;
        component.Speed = newSpeed;
        ReplaceComponent(index, component);
    }

    public void RemoveNavMeshAgent() {
        RemoveComponent(UnitsComponentsLookup.NavMeshAgent);
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
public sealed partial class UnitsMatcher {

    static Entitas.IMatcher<UnitsEntity> _matcherNavMeshAgent;

    public static Entitas.IMatcher<UnitsEntity> NavMeshAgent {
        get {
            if (_matcherNavMeshAgent == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.NavMeshAgent);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherNavMeshAgent = matcher;
            }

            return _matcherNavMeshAgent;
        }
    }
}