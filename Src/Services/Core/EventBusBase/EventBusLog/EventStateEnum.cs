namespace EventBusBase
{
    /// <summary>
    /// 定义事件状态
    /// </summary>
    public enum EventStateEnum
    {
        /// <summary>
        /// 未发布
        /// </summary>
        NotPublished = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// 已发布
        /// </summary>
        Published = 2,
        /// <summary>
        /// 发布失败
        /// </summary>
        PublishedFailed = 3
    }
}
