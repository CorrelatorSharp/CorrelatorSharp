using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace ActivityLogger.Correlation
{
    internal static class ActivityContextTracker
    {
        private static readonly AsyncLocal<Stack<ActivityLogScope>> _activityStack = new AsyncLocal<Stack<ActivityLogScope>>();

        public static ActivityLogScope Current {
            get {
                if (_activityStack.Value != null && _activityStack.Value.Count > 0)
                    return _activityStack.Value.Peek();

                return null;
            }
        }

        public static ActivityLogScope Find(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            if (_activityStack.Value != null && _activityStack.Value.Count > 0)
                return _activityStack.Value.FirstOrDefault(scope => String.Equals(id, scope.Id, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        private static Stack<ActivityLogScope> ActivityStack {
            get {
                if (_activityStack.Value == null)
                    return _activityStack.Value = new Stack<ActivityLogScope>();

                return _activityStack.Value;
            }
        }

        internal static void Start(ActivityLogScope scope)
        {
            ActivityLogScope parent = ActivityStack.Count > 0 ? ActivityStack.Peek() : null;

            if (parent != null)
                scope.ParentId = parent.Id;

            ActivityStack.Push(scope);
        }

        internal static void End(ActivityLogScope scope)
        {
            if (Current == null)
                return;

            if (!ActivityStack.Any(scopeOnTheStack => scope.Id == scopeOnTheStack.Id))
                return;

            ActivityLogScope currentScope = ActivityStack.Pop();
            while(ActivityStack.Count > 0 && currentScope.Id != scope.Id) {
                currentScope = ActivityStack.Pop();
            }
        }
    }
}