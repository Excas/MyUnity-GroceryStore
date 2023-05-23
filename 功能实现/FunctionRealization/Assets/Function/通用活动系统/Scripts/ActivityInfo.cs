using System;
using Function.通用活动系统.Scripts.Data;
using UnityEngine;

namespace Function.通用活动系统.Scripts
{
    /// <summary>
    /// 活动数据
    /// </summary>
    public class ActivityInfo : IActivityEntry
    {
        /// <summary>
        /// 活动分组
        /// </summary>
        public ActivityEntryGroup ActivityEntryGroup { get; set; }

        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; set; }
        
        /// <summary>
        /// 活动本身
        /// </summary>
        public IGameActivity Activity { get; set; }
        
        /// <summary>
        /// 活动配置
        /// </summary>
        public ActivityConfig Config { get; set; }
        
        /// <summary>
        /// 活动分组
        /// </summary>
        public ActivityEntryGroup EntryGroup { get; }
        
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType ActivityType { get; set; }
        
        /// <summary>
        /// 入口Icon
        /// </summary>
        public string EntryIcon { get; set; }
        
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        
        /// <summary>
        /// 排序Id
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 点击回调
        /// </summary>
        public Action OnClick { get; set; }
        
        /// <summary>
        /// 活动Icon
        /// </summary>
        public Sprite ActivityIcon { get; set; }
        
        /// <summary>
        /// 活动预制体
        /// </summary>
        public GameObject ActivityViewPrefab { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; private set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public long EndTime { get; private set; }

        public void InitConfig(ActivityConfig config)
        {
            Config = config;
            ActivityType = (ActivityType) Config.ActivityType;
            ActivityName = Config.Name;
            SortId = Config.Sort;
            ActivityIcon = Config.ActivityIcon;
            ActivityViewPrefab = Config.ActivityPrefab;
        }
    }
}