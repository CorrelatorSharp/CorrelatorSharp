using Machine.Specifications;

namespace CorrelatorSharp.Tests
{
    public class When_static_new_activity_scope_is_opened
    {
        static ActivityScope Scope;

        Because of = () => {
            Scope = ActivityScope.New("test");
        };

        It test_should_be_the_active_scope = () => {
            ActivityScope.Current.ShouldNotBeNull();
            ActivityScope.Current.Id.ShouldEqual(Scope.Id);
        };

        Cleanup cleanup = () => {
            Scope.Dispose();
        };
    }

    public class When_static_new_overwrites_existing
    {
        static ActivityScope ExistingScope1;
        static ActivityScope ExistingScope2;
        static ActivityScope NewScope;

        Establish context = () => {
            ExistingScope1 = new ActivityScope("test_1");
            ExistingScope2 = new ActivityScope("test_2");
        };

        Because of = () => {
            NewScope = ActivityScope.New("test_3");
        };

        It third_should_be_the_active_scope = () => {
            ActivityScope.Current.ShouldNotBeNull();
            ActivityScope.Current.Id.ShouldEqual(NewScope.Id);
        };

        It should_not_contain_second_scope = () => {
            ActivityTracker.Find(ExistingScope2.Id).ShouldBeNull();
        };

        It should_contain_first_scope = () => {
            ActivityTracker.Find(ExistingScope1.Id).ShouldNotBeNull();
        };

        Cleanup cleanup = () => {
            ExistingScope1.Dispose();
            ExistingScope2.Dispose();
            NewScope.Dispose();
        };
    }

    public class When_static_child_create_new
    {
        static ActivityScope ExistingScope;
        static ActivityScope NewScope;

        Establish context = () => {
            ExistingScope = new ActivityScope("test_1");
        };

        Because of = () => {
            NewScope = ActivityScope.Child("test_2");
        };

        It second_should_be_the_active_scope = () => {
            ActivityScope.Current.ShouldNotBeNull();
            ActivityScope.Current.Id.ShouldEqual(NewScope.Id);
        };

        It should_contain_existing_scope = () => {
            ActivityTracker.Find(ExistingScope.Id).ShouldNotBeNull();
        };

        Cleanup cleanup = () => {
            ExistingScope.Dispose();
            NewScope.Dispose();
        };
    }

    public class When_static_create_overwrites_all_existing
    {
        static ActivityScope ExistingScope1;
        static ActivityScope ExistingScope2;
        static ActivityScope NewScope;

        Establish context = () => {
            ExistingScope1 = new ActivityScope("test_1");
            ExistingScope2 = new ActivityScope("test_2");
        };

        Because of = () => {
            NewScope = ActivityScope.Create("test_3");
        };

        It third_should_be_the_active_scope = () => {
            ActivityScope.Current.ShouldNotBeNull();
            ActivityScope.Current.Id.ShouldEqual(NewScope.Id);
        };

        It should_not_contain_second_scope = () => {
            ActivityTracker.Find(ExistingScope2.Id).ShouldBeNull();
        };

        It should_contain_first_scope = () => {
            ActivityTracker.Find(ExistingScope1.Id).ShouldBeNull();
        };

        Cleanup cleanup = () => {
            ExistingScope1.Dispose();
            ExistingScope2.Dispose();
            NewScope.Dispose();
        };
    }
}
