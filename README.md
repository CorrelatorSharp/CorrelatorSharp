## CorrelatorSharp - what is it?


This is the .NET implementation of [CorrelatorSharp](http://correlatorsharp.github.io), which enables context-aware logging and correlation tracking between multiple services and operations. CorrelatorSharp **is async/await safe** and **the current activity flows logically across tasks and threads**.

![](docs/images/diagram.png)


## Get it

|   Branch    |    Status   |
|-------------|-------------|
| NuGet       | [CorrelatorSharp](https://www.nuget.org/packages/CorrelatorSharp/) |
| master      |   [![Build status](https://ci.appveyor.com/api/projects/status/vuorg6sgixm77ugu/branch/master?svg=true)](https://ci.appveyor.com/project/CorrelatorSharp/correlatorsharp/branch/master)  |


## Using it

CorrelatorSharp has out of the box support for various languages and frameworks (.NET, AngularJS, Flask/Python):

* ASP.NET MVC 5 ([link](https://github.com/CorrelatorSharp/CorrelatorSharp.Mvc5))
* ASP.NET Web API ([link](https://github.com/CorrelatorSharp/CorrelatorSharp.WebApi))
* NLog ([link](https://github.com/CorrelatorSharp/CorrelatorSharp.Logging.NLog))
* Application Insights ([link](https://github.com/CorrelatorSharp/CorrelatorSharp.ApplicationInsights))
* RestSharp ([link](https://github.com/CorrelatorSharp/CorrelatorSharp.RestSharp))
* [more here](http://correlatorsharp.github.io)


### Using it standalone

```csharp
using (ActivityScope scope = new ActivityScope("Operation")) {
		Console.WriteLine("Current Activity Id: " + ActivityScope.Current.Id);
	    Console.WriteLine("Current Activity Name: " + ActivityScope.Current.Name);
	    Console.WriteLine("Current Activity ParentId: " + ActivityScope.Current.ParentId);

	using (ActivityScope nestedScope = new ActivityScope("Nested Operation")) {
	    Console.WriteLine("Current Activity Id: " + ActivityScope.Current.Id);
	    Console.WriteLine("Current Activity Name: " + ActivityScope.Current.Name);
	    Console.WriteLine("Current Activity ParentId: " + ActivityScope.Current.ParentId);
    }
}
```

Output:

```
Current Activity Id: 4050a075-51db-4e62-8a92-17720a73045e
Current Activity Name: Operation
Current Activity ParentId:

Current Activity Id: 2d7aa66d-62b4-406d-91e4-ea3305690675
Current Activity Name: Nested Operation
Current Activity ParentId: 4050a075-51db-4e62-8a92-17720a73045e
```

## Extending CorrelatorSharp

### Adding support for logging frameworks

If the logging framework supports injecting additional data at log entry creation time then - look at the Application Insights integration code.

If the logging framework doesn't support injecting additional data - have a look at [CorrelatorSharp.Logging](https://github.com/CorrelatorSharp/CorrelatorSharp.Logging).


### Adding support for other framework

Getting the current activity scope:

```csharp
ActivityScope.Current.Id
ActivityScope.Current.Name
ActivityScope.Current.ParentId
```

Getting the name of the http correlation header:

```csharp
CorrelatorSharp.Headers.CorrelationId
```

Creating a new scope:

```csharp
using CorrelatorSharp;

using (ActivityScope scope = new ActivityScope("Main Operation")) {
    DoWork();
}

```
