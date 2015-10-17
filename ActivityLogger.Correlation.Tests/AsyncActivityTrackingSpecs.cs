using System;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;

namespace ActivityLogger.Correlation.Tests
{
    public class When_a_new_thread_is_created
    {
        static ActivityLogScope Scope;
        static Thread ThreadOne;

        static string ThreadOneScopeId;

        static string ThreadOneChildParentScopeId;

        Establish context = () => {
            Scope = new ActivityLogScope("scope");
            ThreadOne = new Thread(() => {
                ThreadOneScopeId = ActivityLogScope.Current.Id;

                using (ActivityLogScope scope = new ActivityLogScope("ThreadOneScope")) {
                    ThreadOneChildParentScopeId = scope.ParentId;
                }
            });
        };

        Because of = () => {
            ThreadOne.Start();
            ThreadOne.Join();
        };

        It should_flow_the_scope_into_the_thread = () => {
            ThreadOneScopeId.ShouldEqual(Scope.Id);
        };

        It should_connect_the_parent_scope_to_any_new_children_scopes = () => {
            ThreadOneChildParentScopeId.ShouldEqual(Scope.Id);
        };

        Cleanup cleanup = () => {
            Scope.Dispose();
        };
    }

    public class When_using_tasks
    {
        static ActivityLogScope Scope;
        static string TaskScopeId;
        static string TaskChildScopeId;

        Establish context = () => {
            Scope = new ActivityLogScope("scope");
           
        };

        Because of = () => {
            Task.Run(async () => await RootTask()).Wait();
        };

        private static async Task RootTask()
        {
            TaskScopeId = ActivityLogScope.Current.Id;

            await ChildTask();
        }

        private static async Task ChildTask()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            TaskChildScopeId = ActivityLogScope.Current.Id;
        }

        It should_flow_the_scope_into_the_task = () => {
            TaskScopeId.ShouldEqual(Scope.Id);
            TaskChildScopeId.ShouldEqual(TaskScopeId);
        };

        Cleanup cleanup = () => {
            Scope.Dispose();
        };
    }

 
}
