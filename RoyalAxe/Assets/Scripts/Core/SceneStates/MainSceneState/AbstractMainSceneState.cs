using FluentBehaviourTree;
using ProjectUI;

namespace Core.Launcher
{
    /// <summary>
    ///     Содержит ссылки на все ресурсы и контролы необходимые для управления UI и логики сцены.
    ///     Умеет манипулировать данный игрока, моделями логики через API
    /// </summary>
    public abstract class AbstractMetaSceneState : AbstractBTNode, IMainSceneState
    {
        protected readonly IMetaSceneUIViewHolder MaineSceneViewHolder;

        protected AbstractMetaSceneState(IMetaSceneUIViewHolder maineSceneViewHolder)
        {
            MaineSceneViewHolder = maineSceneViewHolder;
        }

        public virtual void EnterState()
        {
            HLogger.LogInfo($"EnterState {NodeName}");
        }

        public virtual void ExitState()
        {
            HLogger.LogInfo($"ExitState {NodeName}");
        }

        public override string NodeName => GetType().Name;
    }
}