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

        public void Decide()
        {
            State newState = OnDecide();
            
            
            if( !EqualityComparer<State>.Default.Equals(newState, CurState))
            {
                OnExitState(CurState);
                this.CurState = newState;
                OnEnterState(CurState);
            }
        }

        public abstract void OnEnterState(State state);
        public abstract void OnExitState(State state);

        public abstract State OnDecide();
    }
}