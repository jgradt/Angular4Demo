# Angular4Demo

Demo project with Angular front end and Asp.net WebApi backend

# Overview:

This repository contains code for a website that queries a backend service.  

A Visual Studio Solution is included along with projects for the client-side angular website (Angular4Demo), the REST services (WebApiDemo), and unit tests for the services (WebApiDemo.UnitTests).  The site and services provide a demonstration for a simple website displaying customers and orders.  

A more thorough explanation of the code can be found [here](https://jgradt.github.io/blog/repos/angular4demo/part1.html).

Some of the technologies involved include:
- [Angular](https://angular.io/)
- [Typescript](https://www.typescriptlang.org/)
- [Bootstrap](https://getbootstrap.com/docs/4.0/getting-started/introduction/)
- [Asp.net Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) WebApi
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) (in-memory database)

# Local Setup:

Initial Setup:
- Download or clone the repository and open it in [Visual Studio 2017](https://www.visualstudio.com/downloads/).  
- Restore the npm packages locally.  
- Rebuild the solution.

Running the solution:
- You should set the Visual Studio solution to have multiple startup projects (Angular4Demo and WebApiDemo)
- Run the solution.  Both projects should start and open in different browser windows.
