# Bookstore Order System

## Overview

This project is a bookstore order system where users can view available books through different API endpoints, place orders, and have those orders delivered via asynchronous programming. This project was both a learning experience and a functional implementation using various technologies.

## Features

- **View Books**: Users can select from a list of available books through REST API endpoints.
- **Place Orders**: Users can place orders for selected books, which are handled asynchronously.
- **CQRS Pattern**: We use the Command Query Responsibility Segregation (CQRS) pattern to separate read and write operations for better scalability and maintainability.
- **Asynchronous Programming with RabbitMQ**: Orders are processed asynchronously using RabbitMQ to decouple services, allowing for better performance and scalability.
- **MediatR Pattern**: MediatR is used to handle in-process messaging between components, following the CQRS pattern to ensure clean separation of concerns.
- **Docker & Docker Compose**: We used Docker and Docker Compose to set up and manage dependencies like SQL Server and RabbitMQ, ensuring the project runs smoothly in isolated containers.
- **Seed Data with Bogus**: For testing and demonstration purposes, the Bogus library was used to generate fake data and seed the database.

## Technologies Used

### Backend
- **.NET Core 87**: The framework for building the APIs and background services.
- **CQRS Pattern**: Separates read and write operations, improving scalability by allowing independent scaling and simplifying the system's structure.
- **MediatR**: A lightweight library that enables request/response and notification-based handling, making the code more modular and testable. MediatR also helps isolate different components, allowing their independent use in different projects.
- **Entity Framework Core**: Used for ORM with SQL Server as the database. EF Core simplifies database operations and mapping, allowing easy integration with C#.
- **SQL Server**: A relational database that efficiently stores and manages data. It's a good fit due to its robustness and scalability in enterprise applications.
- **RabbitMQ**: Used for asynchronous message handling between services. RabbitMQ allows us to handle background tasks and long-running processes, improving system responsiveness.

### Docker
- **Docker**: Used to containerize the application, making it easy to deploy, run, and scale across different environments.
- **Docker Compose**: Helps in orchestrating multiple containers like SQL Server, RabbitMQ, and the application itself, ensuring they run seamlessly together.

### Testing & Seeding
- **Bogus**: A library to generate fake data for testing, allowing us to populate the database with sample data during development.

## Why These Technologies?

- **CQRS & MediatR**: These patterns enable a clean separation of concerns between read and write operations, reducing complexity and making the system more scalable. MediatR makes code modular and reusable across different projects, especially when implementing isolated business logic.
- **Entity Framework & SQL Server**: SQL Server is a powerful relational database, and Entity Framework abstracts the complexities of raw SQL queries, providing a clean, object-oriented interface for database interaction.
- **RabbitMQ**: This ensures asynchronous, reliable message handling for the background order processing. It helps achieve decoupling between services, allowing for easier maintenance and scaling.
- **Docker**: By containerizing the application, we ensure that the app is portable and can be deployed in any environment with minimal configuration, enhancing both the development and deployment workflow.

## Installation & Setup

  To get started with the project, follow these steps:

*Clone the repository**:
	
		git clone https://github.com/arash-a2004/BookList_Rabbit.git
  
Run the API Gateway: After cloning the repository, navigate to the project folder and run the Docker Compose command:

	docker-compose up
This will start all the necessary services, including SQL Server and RabbitMQ.

Run the RestApiService: Once the services are up, run the container program related to RestApiService. This will initialize the API gateway.

Access the Swagger UI: After the services and the API gateway are running, you can access the Swagger UI to explore the available API endpoints via:

	  http://localhost:32769/swagger/index.html
	 





