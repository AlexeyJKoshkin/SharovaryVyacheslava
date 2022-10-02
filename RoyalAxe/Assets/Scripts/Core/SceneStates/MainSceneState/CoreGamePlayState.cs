using System;
using FluentBehaviourTree;
using ProjectUI;

namespace Core.Launcher
{
    public class CoreGamePlayState : AbstractMetaSceneState
    {
        private UnitsEntity _playerUnit;

        public CoreGamePlayState(IMetaSceneUIViewHolder uiViewHolder) : base(uiViewHolder) { }


        public override BehaviourTreeStatus Execute(TimeData time)
        {
            throw new NotImplementedException();
        }
    }
}