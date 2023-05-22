using UnityEngine;

namespace Function.通用活动系统.Scripts
{
    public interface IGameActivity
    {
        ActivityType Type { get; }
        ActivityInfo GetActInfo();
        IActivityEntry ActivityEntry { get; }
        ActivityState State { get; }
        void GetInitData(SCInfo severInfo);
        void GetActivityData(SCInfo severInfo);
        void GetPushData();
        void OnTick(long t);
        bool CanShow();
        bool IsUnlock();
        string GetActivityDes();
        GameObject ShowActivityView(GameObject parent);
        void SetActivityType(ActivityType type);
    }
}