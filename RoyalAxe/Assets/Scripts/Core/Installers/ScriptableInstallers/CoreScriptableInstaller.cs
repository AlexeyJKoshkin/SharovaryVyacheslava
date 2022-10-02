#region

using UnityEngine;

#endregion

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/Core", fileName = "CoreInstaller")]
    public class CoreScriptableInstaller : ScriptableInstaller
    {
        protected override void InstallBindings() { }
    }
}