# Decoupled Communication Between .NET 8 APIs Using RabbitMQ

This repository demonstrates a basic setup for decoupled communication between two .NET 8 APIs, utilizing RabbitMQ as a message broker. This example shows how the **WeatherForecastAPI.Sender** and **WeatherForecastAPI.Receiver** communicate by sending and receiving weather forecast data.

## Project Overview

- **WeatherForecastAPI.Sender**: This API publishes weather forecast data to RabbitMQ.
- **WeatherForecastAPI.Receiver**: This API subscribes to RabbitMQ messages from the Sender and saves the weather data to a PostgreSQL database using Entity Framework Core as the ORM.

## Key Features

- **Decoupled Communication**: Illustrates a loosely-coupled setup for inter-service communication using RabbitMQ.
- **Data Persistence**: Saves received messages to a PostgreSQL database.
- **Containerized Environment**: Uses Docker and Docker Compose for easy deployment and environment setup.

## Prerequisites

- **Docker**: Ensure Docker is installed on your machine to run the containers.

## How to Run

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
2. **Start the Docker Containers**:
   - Use Docker Compose to build and start the services:
   ```bash
   docker-compose up
