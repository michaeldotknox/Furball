# Furball

Furball is a lightweight framework for creating RESTful APIs using .Net.  It is built to run inside OWIN, using as few external dependencies as possible.  Furball uses a combination of attributes, convention and configuration to determine the routes available and what methods are used for the routes.
 
This is still in the early alpha stages, so things will be changing quickly as things are refined and improved.  The milestones will give an indication of what features will be released.  The Roadmap in the wiki will give a clearer indication of what changes will come in the future.

# Quickstart
This guide will walk you through creating a project that uses Furball.  These examples will use Visual Studio 2015 RC and will assume a basic understanding of c# and RESTful APIs.  Please note that this is still an alpha release of Furball, and as such there are some limitations to what the framework can accomplish and you may run into some unexpected issues.  If you have questions, or run into issues, please open a new issue on Github.

## Creating the project
Open Visual Studio 2015 RC
Create a new ASP.Net Web Application project
When asked to select a template, choose the empty template under the ASP.NET 5 Preview Templates
Add a reference to the Furball project, either using NuGet or by manually creating a reference to the Furball Core assembly and the Furball Common Assembly.

## Setting up OWIN to use Furball
Open the startup.cs class and change to code to this:

```
public class Startup
{
	public void Configure(IApplicationBuilder app)
	{
		app.UseFurball()
	}
}
```

## Creating a controller
Add a new class to the project.  Name it SampleController.
Add the following code to the SampleController class:

```
public class SampleController
{
	public int Get(int id)
	{
		return 1;
	}
}
``` 

## Configuring the path
For Furball to know how to direct web requests, you need to tell it which paths map to which methods.  To do this, you need to create a FurballOptions object and tell it to use a ManualPathSource.

```
public class Startup
{
	var options = new FurballOptions
	{
		PathSource = new ManualPathSource().AddPath<SampleController>("/", "get", x => x.Get(Parameter.OfType<int>()))
	}
	
	app.UseFurball(options);
}
```

The AddPath method of the ManualPathSource object takes the type of the controller as a type parameter, and the path, the http method, and an expression that represents the method to be called for that path, along with a list of parameter types that the method will take.  You can use the helper method Parameter.TypeOf to make it easier to pass the list of types.

## Specifying paths with attributes
Furball can detect paths using attributes on the methods of a controller.  Simply add the Path attribute and specify the correct parameters.

```
public class SampleController
{
	[Path(HttpGet, "/")]
	public int Get(int id)
	{
		return 1;
	}
}
```

Then specify the AttributePathSource in the FurballOptions.

```
public class Startup
{
	var options = new FurballOptions
	{
		PathSource = new AttributePathSource()
	}
	
	app.UseFurball(options);
}
```

## Specifying paths manually and with attributes
You can also specify multiple ways of creating paths for Furball to use.  Simple use the AddPathSource method to add any number of path sources.

```
public class Startup
{
	var options = new FurballOptions()
		.AddPathSource(new ManualPathSource().AddPath<SampleController>("/", "get", x => x.Get(Parameter.OfType<int>()))
		.AddPathSource(new AttributePathSource());
	
	app.UseFurball(options);
}
```

Or use the PathSources property.

```
public class Startup
{
	var options = new FurballOptions
	{
		PathSources = new	{
							new ManualPathSource().AddPath<SampleController>("/", "get", x => x.Get(Parameter.OfType<int>())),
							new AttributePathSource())
							}
	} 
}
```

## Creating your own path sources
With Furball, you can specify your paths any way you like.  If you want to use a different scheme to specify parameters in the Url, you can do so.  If you want to create a convention-based method of creating paths, you can do that, too.  All you need to do it to create a new class that implemented IPathSource.

## Handling parameters in the request body
To handle posts or puts, the parameters need to be on the body of the request.  You handle this by adding a [Body] attribute to the parameter of the method:

```
public int Post([Body] TestObject testObject)
{
	return 1;
}
```

## Returning a status code
If you want to return a different status code, return a WebResult object instead of a specific object.  This will allow you to specify a http status code.

```
public WebResult Post[Body] TestObject testObject)
{
	return new WebResult(testObject, HttpStatusCode.Accepted);
}
```