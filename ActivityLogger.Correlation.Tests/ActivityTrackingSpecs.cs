using Machine.Specifications;

namespace ActivityLogger.Correlation.Tests
{
    public class When_new_activity_scope_is_opened
    {
        static ActivityLogScope Scope;

        Because of = () => {
            Scope = new ActivityLogScope("test");
        };

        It should_get_assigned_an_id = () => {
            ActivityLogScope.Current.Id.ShouldNotBeNull();
        };

        It should_be_the_active_one = () => {
            ActivityLogScope.Current.ShouldNotBeNull();
            ActivityLogScope.Current.Id.ShouldEqual(Scope.Id);
        };

        Cleanup cleanup = () => {
            Scope.Dispose();
        };
    }

    public class When_an_activity_scope_is_closed
    {
        static ActivityLogScope Scope;

        Establish context = () => {
            Scope = new ActivityLogScope("test");
        };

        Because of = () => {
            Scope.Dispose();
        };

        It should_be_the_active_one = () => {
            ActivityLogScope.Current.ShouldBeNull();
        };
    }

    public class When_new_nested_activity_scope_is_opened
    {
        static ActivityLogScope ChildScope;
        static ActivityLogScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityLogScope("parent");
        };

        Because of = () => {
            ChildScope = new ActivityLogScope("child");
        };

        It should_be_the_active_one = () => {
            ActivityLogScope.Current.Id.ShouldEqual(ChildScope.Id);
        };

        It should_inherit_the_parent_id = () => {
            ActivityLogScope.Current.ParentId.ShouldEqual(ParentScope.Id);
        };

        Cleanup cleanup = () => {
            ChildScope.Dispose();
            ParentScope.Dispose();
        };
    }

    public class When_a_nested_activity_scope_is_closed
    {
        static ActivityLogScope ChildScope;
        static ActivityLogScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityLogScope("parent");
            ChildScope = new ActivityLogScope("child");
        };

        Because of = () => {
            ChildScope.Dispose();
        };

        It should_be_the_active_one = () => {
            ActivityLogScope.Current.Id.ShouldEqual(ParentScope.Id);
        };

        Cleanup cleanup = () => {
            ParentScope.Dispose();
        };
    }

    public class When_the_parent_activity_scope_is_closed
    {
        static ActivityLogScope ChildScope;
        static ActivityLogScope ParentScope;

        Establish context = () => {
            ParentScope = new ActivityLogScope("parent");
            ChildScope = new ActivityLogScope("child");
        };

        Because of = () => {
            ParentScope.Dispose();
        };

        It should_close_children_scopes = () => {
            ActivityLogScope.Current.ShouldBeNull();
        };

        Cleanup cleanup = () => {
            ChildScope.Dispose();
        };
    }
}
