using System;
using System.Collections.Generic;
using Function.通用活动系统.Scripts.Helper;
using KFramework.Common;

namespace Function.通用活动系统.Scripts
{
    public class GameActivityManager : MonoSingleton<GameActivityManager>
    {
        public Dictionary<ActivityType, IGameActivity> mActivityDict = new Dictionary<ActivityType, IGameActivity>();
        protected override void AwakeEx()
        {
            base.AwakeEx();
            InitAllActivity();
            //vp_Timer.();
        } 
        
        /// <summary>
        /// 仅初始化所有活动
        /// </summary>
        private void InitAllActivity()
        {
            foreach (ActivityType actType in Enum.GetValues(typeof(ActivityType)))
            {
                if (actType==ActivityType.None)
                {
                    continue;
                }
                IGameActivity act = GameActivityHelper.GetActivity(actType);
                act.SetActivityType(actType);
                mActivityDict.Add(actType, act);
            }
        }

        public List<IActivityEntry> GetActivityListByGroup(ActivityEntryGroup group)
        {
            List<IActivityEntry> target=new List<IActivityEntry>();
            foreach (var act in mActivityDict)
            {
                var actInfo = act.Value.GetActInfo();
                if (actInfo.EntryGroup!=group)
                {
                    continue;
                }
                
                if (act.Key == ActivityType.None) 
                {
                    continue;
                }

                if (act.Value.CanShow())
                {
                    target.Add(actInfo);
                }
            }

            target.Sort((a, b) => a.SortId - b.SortId);
            return target;
        }

        public IGameActivity GetGameActivity(ActivityType type)
        {
            mActivityDict.TryGetValue(type, out IGameActivity target);
            return target;
        }
    }
}