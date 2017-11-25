using System;
using System.Linq;
using System.Threading;
using System.Collections.Immutable;

namespace CorrelatorSharp
{
    internal static class ActivityTracker
    {
        private static readonly AsyncLocal<ImmutableStack<ActivityScope>> _activityStack = new AsyncLocal<ImmutableStack<ActivityScope>>();

        public static ActivityScope Current {
            get {
                if (_activityStack.Value != null && _activityStack.Value.Any())
                    return _activityStack.Value.Peek();

                return null;
            }
        }

        public static ActivityScope Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (_activityStack.Value != null && _activityStack.Value.Any())
                return _activityStack.Value.FirstOrDefault(scope => string.Equals(id, scope.Id, StringComparison.OrdinalIgnoreCase));

            return null;
        }

        private static ImmutableStack<ActivityScope> ActivityStack {
            get {
                if (_activityStack.Value == null)
                    return _activityStack.Value = ImmutableStack<ActivityScope>.Empty;

                return _activityStack.Value;
            }

            set
            {
                 _activityStack.Value = value;
            }
        }

        public static void Start(ActivityScope scope)
        {
            var parent = ActivityStack.Any() ? ActivityStack.Peek() : null;

            if (parent != null)
                scope.ParentId = parent.Id;

            ActivityStack = ActivityStack.Push(scope);
        }

        public static void End(ActivityScope scope)
        {
            if (Current == null)
                return;

            if (ActivityStack.All(scopeOnTheStack => scope.Id != scopeOnTheStack.Id))
                return;

            ActivityScope currentScope;
            ActivityStack = ActivityStack.Pop(out currentScope);

            while(ActivityStack.Any() && currentScope.Id != scope.Id) {
                ActivityStack = ActivityStack.Pop(out currentScope);
            }
        }
    }
}