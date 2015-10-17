using System;

namespace ActivityLogger.Correlation
{
    public class ActivityLogScope : IDisposable
    {
        public static ActivityLogScope Find(string id)
        {
            return ActivityContextTracker.Find(id);
        }

        public static ActivityLogScope Current {
            get { return ActivityContextTracker.Current; }
        }

        public ActivityLogScope(string name)
            : this(name, Guid.NewGuid().ToString())
        {
        }

        public ActivityLogScope(string name, string id, string parentId)
            :this(name, id)
        {
            ParentId = parentId;
        }

        public ActivityLogScope(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"{nameof(id)} is null or empty.", nameof(id));

            Name = name;
            Id = id;

            ActivityContextTracker.Start(this);
        }

        public string Id { get; private set; }

        public string ParentId { get; internal set; }

        public string Name { get; set; }

        public void Dispose()
        {
            ActivityContextTracker.End(this);
        }
    }
}