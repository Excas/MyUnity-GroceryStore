using System;

namespace Function.通用活动系统.Scripts
{
    /// <summary>
    /// 活动入口
    /// </summary>
    public interface IActivityEntry
    {
        ActivityEntryGroup ActivityEntryGroup { get; }
        int ActivityId { get; }
        ActivityType ActivityType { get; }
        string EntryIcon { get; }
        string ActivityName { get; }
        IGameActivity Activity { get; }
        int SortId { get; }
        Action OnClick { get; }
    }
}