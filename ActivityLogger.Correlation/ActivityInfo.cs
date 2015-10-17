using System;

namespace ActivityLogger.Correlation
{
    public class ActivityInfo
    {
        public ActivityInfo(string description)
            : this(description, Guid.NewGuid())
        {
        }

        public ActivityInfo(string description, Guid id)
        {
            Description = description;
            Id = id;
        }

        public ActivityInfo(string description, Guid id, Guid parentId)
            :this(description, id)
        {
            Description = description;
            Id = id;
        }

        public Guid Id { get; private set; }
        public Guid ParentId { get; private set; }

        public string Description { get; set; }
    }
}