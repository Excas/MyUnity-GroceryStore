using Function.通用活动系统.Scripts.GameActivity;
using UnityEngine;

namespace Function.通用活动系统.Scripts.Helper
{
    public class GameActivityHelper
    {
        public static IGameActivity GetActivity(ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.ActivityDemo1: return new Demo1Activity();
                case ActivityType.ActivityDemo2: return new Demo2Activity();
                case ActivityType.Activity_Lottery: return new Demo3Activity();
                default:
                    return null;
            }
        }
    }
}