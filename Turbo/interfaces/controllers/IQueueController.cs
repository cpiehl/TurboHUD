using System;
using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface IQueueItem
    {
        DateTime QueuedOn { get; }
        TimeSpan LifeTime { get; }
    }

    public interface IQueueController
    {
        void AddItem(IQueueItem item);
        IEnumerable<T> GetItems<T>() where T : IQueueItem;
    }
}