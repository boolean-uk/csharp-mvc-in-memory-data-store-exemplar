# C# API Model View Controller

## Learning Objectives
- Use the Model View Controller architecture to organise source code

## Introduction

We didn't do much to organise our code in the previous exercise - I bet you can imagine how messy it would get building an app with hundreds of classes all in the same package!

To remedy this, we often organise our code into their own projects within the solution. This is another form of encapsulation.

One common technique is named "Model, View, Controller". This involves separating our code into "layers".

### Model
This is our data layer. It describes what data looks like (e.g. Users, Cars, Products). 

### View
This is what the client interacts with. It could be a HTML page, a terminal output or a JSON response from a RESTful API. With API's, the view (JSON) is a representation of a data model.

### Controller
This layer is responsible for translating interactions from the View layer into logic in our program. If a user clicks a button on a view, the controller decides what to do with that action. Consider the scenario below:

- User clicks on a "Create Product" button in a view
- A controller class might validate that the user entered a valid product name before telling the Model layer to create the product
- A repository class takes care of creating the Product model in a database and lets the controller know when it's done
- The controller notifies the view that the product was created
- The view displays a "Success!" message to the user

This approach to designing a system allows us to keep things organised and modular with each layer responsible for very specific tasks. If we wanted to change our database later, we'd only need to change the Model layer without touching the Controller or View layers.

## Instructions

1. Fork this repository
2. Clone your fork to your machine
3. Open the project in Visual Studio
4. Take a look in the `bagels` package to see an example of how an API might be structured using the MVC approach (we are using in this exercise the ASP.NET, to be able to visualize the api endpoints and the data in the browser).
5. Create your own Products classes:
	Products model
	ProductRepository repository class with some CRUD
	Inject the repository into the ProductsController and write controller CRUD methods
	
Use sample API code provided.  [implement this API spec following an MVC approach](https://boolean-uk.github.io/csharp-mvc-in-memory-data-store/).

## Extensions

[This API spec](https://boolean-uk.github.io/csharp-mvc-in-memory-data-store/extensions.html) contains some additions to the core spec, including adding error responses under certain scenarios and allowing clients to optionally filter products by category.
