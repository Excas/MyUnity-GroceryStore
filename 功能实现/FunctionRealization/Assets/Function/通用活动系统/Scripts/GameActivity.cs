using System;
using Common.CommonScripts;
using Function.通用活动系统.Scripts.Data;
using UnityEngine;

namespace Function.通用活动系统.Scripts
{
    public abstract class GameActivity<INFO> : KFSM<ActivityState>, IGameActivity where INFO : ActivityInfo
    {
        /// <summary>
        /// 活动状态变更
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 弹出活动界面
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual GameObject ShowActivityView(GameObject parent)
        {
            //demo
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
            }
            return GameObject.Instantiate(Info.ActivityViewPrefab, parent.transform, false);
        }

        // /// <summary>
        // /// 获取活动界面预制体
        // /// </summary>
        // /// <returns></returns>
        // protected virtual GameObject AddActivityItem()
        // {
        //     return null;
        // }

        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType Type { get; private set; }

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <returns></returns>
        public ActivityInfo GetActInfo()
        {
            return Info;
        }

        /// <summary>
        /// 活动入口信息
        /// </summary>
        public IActivityEntry ActivityEntry { get; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityState State { get; }

        /// <summary>
        /// 活动信息
        /// </summary>
        public INFO Info { get; private set; }

        /// <summary>
        /// 服务器同步的活动基础信息
        /// </summary>
        /// <param name="severInfo"></param>
        public virtual void GetInitData(SCInfo severInfo)
        {

        }

        /// <summary>
        /// 服务器 针对不同活动 同步各个活动信息
        /// </summary>
        /// <param name="severInfo"></param>
        public virtual void GetActivityData(SCInfo severInfo)
        {

        }

        /// <summary>
        /// 服务器推送信息
        /// </summary>
        public virtual void GetPushData()
        {

        }

        /// <summary>
        /// Tike
        /// </summary>
        /// <param name="t"></param>
        public virtual void OnTick(long t)
        {
            OnChange();
        }

        /// <summary>
        /// 是否显示活动
        /// </summary>
        /// <returns></returns>
        public virtual bool CanShow()
        {
            return true;
            return State == ActivityState.Open;
        }

        /// <summary>
        /// 活动是否解锁
        /// </summary>
        /// <returns></returns>
        public virtual bool IsUnlock()
        {
            return true;
        }

        /// <summary>
        /// 活动描述
        /// </summary>
        /// <returns></returns>
        public virtual string GetActivityDes()
        {
            return "";
        }

        /// <summary>
        /// 设置活动类型
        /// </summary>
        /// <param name="type"></param>
        public void SetActivityType(ActivityType type)
        {
            Type = type;
            SetActInitData();
        }

        private void SetActInitData()
        {
            Info = Activator.CreateInstance<INFO>();
            GameActivityConfig configList = Resources.Load<GameActivityConfig>("GameActivityConfig");
            var config = configList.ActivityConfigList.Find(a => (ActivityType) a.ActivityType == Type);
            if (config != null)
            {
                Info.InitConfig(config);
            }
            Info.Activity = this;
        }
    }
}