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
