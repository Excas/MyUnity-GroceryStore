using System.Collections.Generic;

namespace Common.CommonScripts
{
    public abstract class KFSM<State> where State: struct
    {
        public State CurState { get; private set; }
        public void Clear()
        {
            CurState = default(State);
        }

        public void Change()
        {
            State newState = OnChange();
            if( !EqualityComparer<State>.Default.Equals(newState, CurState))
            {
                OnExitState(CurState);
                this.CurState = newState;
                OnEnterState(CurState);
            }
        }

        public virtual void OnEnterState(State state)
        {
            
        }

        public virtual void OnExitState(State state)
        {
            
        }

        public abstract State OnChange();
    }
}