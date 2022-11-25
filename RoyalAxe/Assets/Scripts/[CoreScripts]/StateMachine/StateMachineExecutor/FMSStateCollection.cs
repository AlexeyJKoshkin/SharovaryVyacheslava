using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class FMSStateCollection<T> where T : IFMSState
    {
        private readonly List<T> _states;

        public FMSStateCollection(IEnumerable<T> fmsStates)
        {
            _states = new List<T>(fmsStates);
        }

        public FMSStateCollection()
        {
            _states = new List<T>();
        }

        public T Current { get; private set; }

        public void SetState<TNewState>() where TNewState : IFMSState
        {
            var newState = _states.FirstOrDefault(o => o is TNewState);
            SetNewCurrent(newState);
        }

        public void SetCurrentFirst()
        {
            var newState = _states.FirstOrDefault();
            SetNewCurrent(newState);
        }

        private void SetNewCurrent(T newState)
        {
            if (newState == null)
            {
                return;
            }

            if (Current == null)
            {
                SetNewCurrentState();
                return;
            }

            Current.ExitState();
            SetNewCurrentState();

            void SetNewCurrentState()
            {
                Current = newState;
                Current.EnterState();
            }
        }


        public void SetCurrent(T newState)
        {
            SetNewCurrent(newState);
        }

        public IEnumerable<T> All()
        {
            return _states;
        }
    }
}