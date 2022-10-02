using System;
using UnityEngine;

namespace FluentBehaviourTree 
{
    public class TimerNode : AbstractBTNode
    {
        private float _time;

        public bool IsPause;
        public bool IsDone => _time <=0;

        public float Timer
        {
            get => _time;
            set { _time = value; }
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            if(!IsPause)
                _time -= time.deltaTime;
            return _time > 0 ? BehaviourTreeStatus.Running : BehaviourTreeStatus.Success;
        }

        public TimerNode(float time, string name = "Таймер")
        {
            _time    = time;
            NodeName = name;
        }

        public override string NodeName { get; }
    }
}