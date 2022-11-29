using System;
using UnityEngine;

namespace FluentBehaviourTree 
{
    public class TimerNode : AbstractBTNode
    {
        private float _time,
                      _counter;

        public bool IsPause;
        public bool IsDone => _counter <=0;

        public float Timer
        {
            get => _counter;
            set { _counter = value; }
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            if(!IsPause)
                _counter -= time.deltaTime;
            return _time > 0 ? BehaviourTreeStatus.Running : BehaviourTreeStatus.Success;
        }

        public TimerNode(float time, string name = "Таймер")
        {
            _counter    = time;
            _time = time;
            NodeName = name;
        }

        public override string NodeName { get; }

        public void ResetTimer()
        {
            _counter = _time;
        }
    }
}