namespace Function.通用活动系统.Scripts
{
    /// <summary>
    /// 活动数据
    /// </summary>
    public class ActivityInfo : IActivityEntry
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; }
        
        /// <summary>
        /// 活动本身
        /// </summary>
        public IGameActivity Activity { get; }
        
        /// <summary>
        /// 活动分组
        /// </summary>
        public ActivityEntryGroup EntryGroup { get; }
        
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType ActivityType { get; }
        
        /// <summary>
        /// 入口Icon
        /// </summary>
        public string EntryIcon { get; }
        
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; }
        
        /// <summary>
        /// 排序Id
        /// </summary>
        public int SortId { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; private set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public long EndTime { get; private set; }
    }
}