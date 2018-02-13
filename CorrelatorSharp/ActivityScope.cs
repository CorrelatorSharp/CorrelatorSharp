using System;

namespace CorrelatorSharp
{
    public class ActivityScope : IDisposable
    {
        /// <summary>
        /// The current active <see cref="ActivityScope"/> for the current context.
        /// </summary>
        public static ActivityScope Current => ActivityTracker.Current;

        /// <summary>
        /// Creates new <see cref="ActivityScope"/> for given name with a <see cref="Guid"/>.<see cref="Guid.NewGuid"/> as ID.<para />
        /// Sets this instance to the current active <see cref="ActivityScope"/> for context, see <see cref="Current"/>
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        public ActivityScope(string name)
            : this(name, Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Creates new <see cref="ActivityScope"/> for given name, ID, and parent ID. Please ensure IDs are unique.<para />
        /// Sets this instance to the current active <see cref="ActivityScope"/> for context, see <see cref="Current"/>
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        /// <param name="id">Please ensure IDs are unique.</param>
        /// <param name="parentId">Please ensure IDs are unique.</param>
        public ActivityScope(string name, string id, string parentId)
            :this(name, id)
        {
            ParentId = parentId;
        }

        /// <summary>
        /// Creates new <see cref="ActivityScope"/> for given name and ID. Please ensure IDs are unique.<para />
        /// Sets this instance to the current active <see cref="ActivityScope"/> for context, see <see cref="Current"/>
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        /// <param name="id">Please ensure IDs are unique.</param>
        public ActivityScope(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"{nameof(id)} is null or empty.", nameof(id));
            }

            Name = name;
            Id = id;

            ActivityTracker.Start(this);
        }

        /// <summary>
        /// The given ID for the <see cref="ActivityScope"/>.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The given Parent ID for the <see cref="ActivityScope"/>.
        /// </summary>
        public string ParentId { get; internal set; }

        /// <summary>
        /// The given Name for the <see cref="ActivityScope"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ends the <see cref="ActivityScope"/>.<see cref="Current"/> and 
        /// creates new instance of <see cref="ActivityScope"/>.<see cref="Current"/> for given ID and name.<para />
        /// <para />
        /// Use only if you wish to clear <see cref="ActivityScope"/>.<see cref="Current"/>, you may be looking for <see cref="ActivityScope"/>.<see cref="Child"/>.
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        /// <param name="id">Optional, will default to <see cref="Guid"/>.<see cref="Guid.NewGuid"/>. Please ensure provided IDs are unique.</param>
        /// <returns></returns>
        public static ActivityScope New(string name, string id = null)
        {
            Current?.Dispose();
            return new ActivityScope(name, id ?? Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Creates new instance of <see cref="ActivityScope"/>.<see cref="Current"/> for given ID and name,
        /// with ParentID of previous <see cref="ActivityScope"/>.<see cref="Current"/>.<para />
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        /// <param name="id">Optional, will default to <see cref="Guid"/>.<see cref="Guid.NewGuid"/>. Please ensure provided IDs are unique.</param>
        /// <returns></returns>
        public static ActivityScope Child(string name, string id = null)
        {
            return new ActivityScope(name, id ?? Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Clears all parents for <see cref="ActivityScope"/>, and
        /// creates new instance of <see cref="ActivityScope"/>.<see cref="Current"/> for given ID and name.<para />
        /// <para />
        /// Use only if you wish to clear all active <see cref="ActivityScope"/>, you may be looking for <see cref="ActivityScope"/>.<see cref="Child"/>.
        /// </summary>
        /// <param name="name">Human readable name for <see cref="ActivityScope"/></param>
        /// <param name="id">Optional, will default to <see cref="Guid"/>.<see cref="Guid.NewGuid"/>. Please ensure provided IDs are unique.</param>
        /// <returns></returns>
        public static ActivityScope Create(string name, string id = null)
        {
            ActivityTracker.Clear();
            return new ActivityScope(name, id ?? Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Disposes the instance and ends <see cref="ActivityScope"/>.<see cref="Current"/>.
        /// </summary>
        public void Dispose()
        {
            ActivityTracker.End(this);
        }
    }
}