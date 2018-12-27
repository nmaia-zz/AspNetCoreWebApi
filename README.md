# AspNetCoreWebApi
A sample project using Asp Net Core 2.1 WebApi and EF Core.

## Testing the project

- I've setup swagger in order to deliver a friendly way to test the project. When you run it, the index page of the API is the swagger documentation.

![Swagger](https://github.com/nmaia/AspNetCoreWebApi/blob/master/Docs/Images/Swagger.png)

- But if you want to use postman to test the project, you must turn off the SSL certificate validation in the postman settings section.

![Swagger](https://github.com/nmaia/AspNetCoreWebApi/blob/master/Docs/Images/postman.png)

## Some stuff we've used in the project

- Brotli Algorithm: to handle API response compression;
- RestSharp: to connect with the external API [swApi.co](https://swapi.co)
- xUnit: to develop unit tests;
- Swagger: to provide the API documentation;
- AspNet Core 2.1;
- EntityFramework Core;
- SQL Server Express;
