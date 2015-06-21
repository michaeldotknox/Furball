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
		PathSource = new ManualPathSource().AddPath<SampleController>("/", "Get", "get", new object[] {})
	}
	
	app.UseFurball(options);
}
```

The AddPath method of the ManualPathSource object takes the type of the controller as a type parameter, and the path, the name of the method, the http method, and an array objects that represents the parameters in teh requested method.
   