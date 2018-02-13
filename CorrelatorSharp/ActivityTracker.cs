using System;
using System.Linq;
using System.Threading;
using System.Collections.Immutable;

namespace CorrelatorSharp
{
    public static class ActivityTracker
    {
        private static readonly AsyncLocal<ImmutableStack<ActivityScope>> _activityStack = new AsyncLocal<ImmutableStack<ActivityScope>>();

        internal static ActivityScope Current
        {
            get
            {
                if (_activityStack.Value != null && _activityStack.Value.Any())
                {
                    return _activityStack.Value.Peek();
                }

                return null;
            }
        }

        public static ActivityScope Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            if (_activityStack.Value != null && _activityStack.Value.Any())
            {
                return _activityStack.Value.FirstOrDefault(scope => string.Equals(id, scope.Id, StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        internal static ImmutableStack<ActivityScope> ActivityStack
        {
            get
            {
                if (_activityStack.Value == null)
                {
                    return _activityStack.Value = ImmutableStack<ActivityScope>.Empty;
                }

                return _activityStack.Value;
            }
            set => _activityStack.Value = value;
        }

        internal static void Start(ActivityScope scope)
        {
            var parent = ActivityStack.Any() ? ActivityStack.Peek() : null;
            if (parent != null)
            {
                scope.ParentId = parent.Id;
            }

            ActivityStack = ActivityStack.Push(scope);
        }

        internal static void End(ActivityScope scope)
        {
            if (Current == null)
            {
                return;
            }

            if (ActivityStack.All(scopeOnTheStack => scope.Id != scopeOnTheStack.Id))
            {
                return;
            }

            ActivityStack = ActivityStack.Pop(out ActivityScope currentScope);

            while (ActivityStack.Any() && currentScope.Id != scope.Id)
            {
                ActivityStack = ActivityStack.Pop(out currentScope);
            }
        }

        internal static void Clear()
        {
            if (Current == null)
            {
                return;
            }

            ActivityStack = ImmutableStack<ActivityScope>.Empty;
        }
    }
}