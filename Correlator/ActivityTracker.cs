using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Correlator
{
    internal static class ActivityTracker
    {
        private static readonly AsyncLocal<Stack<ActivityScope>> _activityStack = new AsyncLocal<Stack<ActivityScope>>();

        public static ActivityScope Current {
            get {
                if (_activityStack.Value != null && _activityStack.Value.Count > 0)
                    return _activityStack.Value.Peek();

                return null;
            }
        }

        public static ActivityScope Find(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            if (_activityStack.Value != null && _activityStack.Value.Count > 0)
                return _activityStack.Value.FirstOrDefault(scope => String.Equals(id, scope.Id, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        private static Stack<ActivityScope> ActivityStack {
            get {
                if (_activityStack.Value == null)
                    return _activityStack.Value = new Stack<ActivityScope>();

                return _activityStack.Value;
            }
        }

        public static void Start(ActivityScope scope)
        {
            ActivityScope parent = ActivityStack.Count > 0 ? ActivityStack.Peek() : null;

            if (parent != null)
                scope.ParentId = parent.Id;

            ActivityStack.Push(scope);
        }

        public static void End(ActivityScope scope)
        {
            if (Current == null)
                return;

            if (!ActivityStack.Any(scopeOnTheStack => scope.Id == scopeOnTheStack.Id))
                return;

            ActivityScope currentScope = ActivityStack.Pop();
            while(ActivityStack.Count > 0 && currentScope.Id != scope.Id) {
                currentScope = ActivityStack.Pop();
            }
        }
    }
}