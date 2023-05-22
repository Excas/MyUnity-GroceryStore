using System;
using Common.CommonScripts;
using UnityEngine;

namespace Function.通用活动系统.Scripts
{
    public class GameActivity<INFO> :KFSM<ActivityState>,IGameActivity where INFO : ActivityInfo
    {
        public override ActivityState OnChange()
        {
            if (Info == null || Info.ActivityType == ActivityType.None)
            {
                return ActivityState.None;
            }

            long now = DateTime.UtcNow.Millisecond;
            if (Info.EndTime > now)
            {
                return ActivityState.End;
            }
            else
            {
                return ActivityState.Open;
            }
        }

        public ActivityType Type { get; private set; }

        public ActivityInfo GetActInfo()
        {
            return Info;
        }

        public IActivityEntry ActivityEntry { get; }
        public ActivityState State { get; }
        public INFO Info { get; private set; }
        public virtual void GetInitData(SCInfo severInfo)
        {
            
        }

        public virtual void GetActivityData(SCInfo severInfo)
        {
            
        }

        public virtual void GetPushData()
        {
            
        }

        public virtual void OnTick(long t)
        {
            OnChange();
        }

        public virtual bool CanShow()
        {
            return State == ActivityState.Open;
        }

        public virtual bool IsUnlock()
        {
            return true;
        }

        public virtual string GetActivityDes()
        {
            return "";
        }

        public GameObject ShowActivityView(GameObject parent)
        {
            GameObject o = AddActivityItem();
            return null;
        }

        protected virtual GameObject AddActivityItem()
        {
            return null;
        }

        public void SetActivityType(ActivityType type)
        {
            Type = type;
        }
    }
}