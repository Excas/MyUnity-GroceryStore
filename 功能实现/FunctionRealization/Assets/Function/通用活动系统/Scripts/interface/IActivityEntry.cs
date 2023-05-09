namespace Function.通用活动系统.Scripts
{
    /// <summary>
    /// 活动入口
    /// </summary>
    public interface IActivityEntry
    {
        int ActivityId { get; }

        ActivityType ActivityType { get; }
        
        string EntryIcon { get; }
        
        string ActivityName { get; }

        IGameActivity Activity { get; }
    }
}