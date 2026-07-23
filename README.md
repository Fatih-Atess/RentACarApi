# Rent-A-Car REST API

A modern, fast, and scalable RESTful API built to manage a car rental system. This project was developed to demonstrate clean architecture, raw SQL database interactions, and strict transaction management.

## Technologies Used
* **Framework:** C# ASP.NET Core
* **Database:** MySQL
* **ORM:** Dapper (Micro-ORM used instead of Entity Framework for high-performance raw SQL execution)
* **Architecture:** Repository Pattern applying SOLID principles
* **API Documentation:** Swagger UI

## Features
* **List Available Cars:** `GET /api/cars?status=available` fetches all vehicles currently available for rent.
* **Rent a Car:** `POST /api/rentals` processes a rental request. It utilizes **Database Transactions (Commit/Rollback)** to ensure the car's status is updated and the rental record is created simultaneously.
* **Conflict Handling:** Automatically rejects overlapping rental dates for the same vehicle with a `409 Conflict` response.

## Setup Instructions
1. Clone the repository: `git clone https://github.com/your-username/RentACarApi.git`
2. Open your MySQL database and run the schema provided in the project to create the `RentACarDb` database.
3. Open `appsettings.json` and update the `DefaultConnection` string with your local MySQL password.
4. Run the project in Visual Studio. Swagger UI will open automatically to test the endpoints.

# RentACarUi

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 21.1.4.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Vitest](https://vitest.dev/) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
