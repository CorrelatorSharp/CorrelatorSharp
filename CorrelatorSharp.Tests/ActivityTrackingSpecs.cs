using System;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;

namespace CorrelatorSharp.Tests
{
    public class When_new_activity_scope_is_opened
    {
        static ActivityScope Scope;

        Because of = () => {
            Scope = new ActivityScope("test");
        };

        It should_get_assigned_an_id = () => {
            ActivityScope.Current.Id.ShouldNotBeNull();
        };

        It should_be_the_active_one = () => {
            ActivityScope.Current.ShouldNotBeNull();
            ActivityScope.Current.Id.ShouldEqual(Scope.Id);
        };

        Cleanup cleanup = () => {
            Scope.Dispose();
        };
    }

    public class When_an_activity_scope_is_closed
    {
        static ActivityScope Scope;

        Establish context = () => {
            Scope = new ActivityScope("test");
        };

        Because of = () => {
            Scope.Dispose();
        };

        It should_be_the_active_one = () => {
            ActivityScope.Current.ShouldBeNull();
        };
    }

    public class When_new_nested_activity_scope_is_opened
    {
        static ActivityScope ChildScope;
        static ActivityScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityScope("parent");
        };

        Because of = () => {
            ChildScope = new ActivityScope("child");
        };

        It should_be_the_active_one = () => {
            ActivityScope.Current.Id.ShouldEqual(ChildScope.Id);
        };

        It should_inherit_the_parent_id = () => {
            ActivityScope.Current.ParentId.ShouldEqual(ParentScope.Id);
        };

        Cleanup cleanup = () => {
            ChildScope.Dispose();
            ParentScope.Dispose();
        };
    }

    public class When_a_nested_activity_scope_is_closed
    {
        static ActivityScope ChildScope;
        static ActivityScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityScope("parent");
            ChildScope = new ActivityScope("child");
        };

        Because of = () => {
            ChildScope.Dispose();
        };

        It should_be_the_active_one = () => {
            ActivityScope.Current.Id.ShouldEqual(ParentScope.Id);
        };

        Cleanup cleanup = () => {
            ParentScope.Dispose();
        };
    }

    public class When_running_multiple_async_tasks
    {
        Because of = () => {
            using (var scope = new ActivityScope("Root task scope", "Bob")) {
                var barrier = new Barrier(2);
                var t1 = Task.Run((Action) (() => ChildTaskA(barrier, scope.Id)));
                var t2 = Task.Run((Action) (() => ChildTaskB(barrier, scope.Id)));
                Task.WhenAll(t1, t2).GetAwaiter().GetResult();
            }
        };

        It should_preserve_correct_parents = () => {
            // Expectations in child tasks
        };

        It should_not_pop_an_empty_stack = () => {
            // Expectations in child tasks
        };

        private static void ChildTaskA(Barrier barrier, string expectedParentId)
        {
            using (var scopeA = new ActivityScope("Child task A", "Bob 1")) {
                barrier.SignalAndWait();
                scopeA.ParentId.ShouldEqual(expectedParentId);
            }
        }

        private static void ChildTaskB(Barrier barrier, string expectedParentId)
        {
            using (var scopeB = new ActivityScope("Child task B", "Bob 2"))
            {
                barrier.SignalAndWait();
                scopeB.ParentId.ShouldEqual(expectedParentId);
            }
        }
    }

    public class When_the_parent_activity_scope_is_closed
    {
        static ActivityScope ChildScope;
        static ActivityScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityScope("parent");
            ChildScope = new ActivityScope("child");
        };

        Because of = () => {
            ParentScope.Dispose();
        };

        It should_close_children_scopes = () => {
            ActivityScope.Current.ShouldBeNull();
        };

        Cleanup cleanup = () => {
            ChildScope.Dispose();
        };
    }
}
