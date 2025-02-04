Project Name : OneIncSample

Implemented based on RESTful architecture with In-memory DB.
NuGet packages used:
	Microsoft.EntityFrameworkCore.InMemory for in-memory database.
	Serilog.AspNetCore for logging	

1. Resource-Based URIs:
		URIs are designed to represent resources.
		Example: /api/users and /api/users/{id}.
		HTTP Methods:

2. Implemented standard HTTP methods to interact with the resources.
	GET /api/users: Retrieves a list of users.
	GET /api/users/{id}: Retrieves a specific user by ID.
	POST /api/users: Creates a new user.
	PUT /api/users/{id}: Updates an existing user by ID.
	DELETE /api/users/{id}: Deletes a user by ID.

3. JSON Format:
	The API uses JSON as the data format for requests and responses, ensuring that data is easily readable and transmittable.
	
4. Statelessness:
	Each request contains all necessary information, and no client state is stored on the server between requests.

5. Logging:
	Serilog is used for logging to capture and log information, warnings, and errors, which helps in monitoring and debugging the application.

6. Validation
	Validating incoming requests in an API is crucial for ensuring data integrity and security. In ASP.NET Core, leverage model validation to check the incoming data. 	
	Define Data Transfer Objects (DTOs):
		- Create User.cs model DTOs with validation attributes to specify the validation rules.
	Use Validation Attributes:
		- Apply validation attributes from the `System.ComponentModel.DataAnnotations` namespace to DTO properties.