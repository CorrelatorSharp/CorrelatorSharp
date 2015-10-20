using System;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;

namespace CorrelatorSharp.Tests
{
    public class When_a_new_thread_is_created
    {
        static ActivityScope Scope;
        static Thread ThreadOne;

        static string ThreadOneScopeId;

        static string ThreadOneChildParentScopeId;

        Establish context = () => {
            Scope = new ActivityScope("scope");
            ThreadOne = new Thread(() => {
                ThreadOneScopeId = ActivityScope.Current.Id;

                using (ActivityScope scope = new ActivityScope("ThreadOneScope")) {
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
        static ActivityScope Scope;
        static string TaskScopeId;
        static string TaskChildScopeId;

        Establish context = () => {
            Scope = new ActivityScope("scope");
           
        };

        Because of = () => {
            Task.Run(async () => await RootTask()).Wait();
        };

        private static async Task RootTask()
        {
            TaskScopeId = ActivityScope.Current.Id;

            await ChildTask();
        }

        private static async Task ChildTask()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            TaskChildScopeId = ActivityScope.Current.Id;
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
