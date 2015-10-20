using System;

namespace Correlator
{
    public class ActivityScope : IDisposable
    {
        public static ActivityScope Current {
            get { return ActivityTracker.Current; }
        }

        public ActivityScope(string name)
            : this(name, Guid.NewGuid().ToString())
        {
        }

        public ActivityScope(string name, string id, string parentId)
            :this(name, id)
        {
            ParentId = parentId;
        }

        public ActivityScope(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"{nameof(id)} is null or empty.", nameof(id));

            Name = name;
            Id = id;

            ActivityTracker.Start(this);
        }

        public string Id { get; private set; }

        public string ParentId { get; internal set; }

        public string Name { get; set; }

        public void Dispose()
        {
            ActivityTracker.End(this);
        }
    }
}