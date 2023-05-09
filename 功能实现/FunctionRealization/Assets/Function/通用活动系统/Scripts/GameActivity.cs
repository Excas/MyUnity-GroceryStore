using Common.CommonScripts;

namespace Function.通用活动系统.Scripts
{
    public class GameActivity<INFO> :KFSM<ActivityState> where INFO : ActivityInfo
    {
        public override void OnEnterState(ActivityState state)
        {
            
        }

        public override void OnExitState(ActivityState state)
        {
           
        }

        public override ActivityState OnDecide()
        {
            throw new System.NotImplementedException();
        }
    }
}