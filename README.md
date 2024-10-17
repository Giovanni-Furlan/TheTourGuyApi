# The Tour Guy API

## Overview

This .NET solution is a REST API designed to search and filter tour products from multiple sources, both internal and external. The API can handle products from various suppliers, each with different data structures, by using a dynamic JSON parser and a configuration file to map different product properties to a unified model. The goal is to provide a flexible, scalable solution that supports adding new suppliers and their product formats easily.

The task was part of an interview exercise, where the main focus is to integrate multiple product sources and provide a unified response through a single endpoint.

## Functional Requirements

The API supports searching through three data sources represented as JSON files:

- **TheTourGuy.json**
- **SomeOtherGuy.json**
- **TheBigGuy.json**

### Key Features

- **Dynamic JSON Parsing**: Products are parsed from the JSON files using dynamic objects.
- **Configurable Mappings**: Different suppliers have different JSON structures. This project uses a configuration file (`mapping.json`) to map supplier-specific fields (like name, price, and description) to a unified product model.
- **Flexible Querying**: The API accepts multiple filters such as number of guests, product name, destination, supplier, and price, and paginates the results.
- **Unified Response**: Despite the varied formats of the JSON sources, the API returns a standardized response for all products.
- **Docker**: The solution can be containerized and run using Docker. 

## Technologies Used

- **.NET 8**
- **Newtonsoft.Json** for JSON parsing
- **Microsoft.Extensions.Logging** for logging
- **Asynchronous Processing** for efficient parsing of large JSON files
- **Docker** for containerized deployment

## Extending the API

To add a new supplier, follow these steps:

Add the new supplier's JSON file to the Resources folder.
Update mapping.json with the new supplier's JSON structure, mapping its fields to the common model.
No additional code changes are required.

## Potential Improvements

While the current solution meets the basic requirements of parsing multiple product sources and returning unified results, several enhancements can be made to improve its robustness, maintainability, and performance:

1. **Authentication and Authorization**: The API currently does not implement any form of authentication or authorization. Adding authentication mechanisms such as OAuth2, JWT tokens, or API keys would ensure that only authorized users can access the API.

2. **Unit Tests**: Although some unit tests are included, there is room to increase coverage. Mocking services like the file system and logger (using `Moq` or a similar library) will allow more thorough testing of different scenarios, such as missing files or invalid JSON formats.

3. **Error Handling and Validation**: Improve error handling to provide more granular feedback on parsing errors, invalid JSON structures, or configuration issues. This could be achieved with custom exceptions and better validation of input data.

4. **Sorting**: Adding sorting options (e.g., by price, name, or rating) would improve the user experience. This can be easily implemented by modifying the query logic.

5. **Logging Enhancements**: Improve logging to track more specific events, such as when products are successfully parsed or filtered. Structured logging with tools like Serilog would enhance traceability and simplify debugging.

6. **Database Integration**: Instead of relying on JSON files, the API can be extended to pull data from a relational database (e.g., SQL Server or PostgreSQL) for better scalability. This would allow for easier updates and more dynamic product management.

7. **Configuration Management**: Move configurations, such as the `mapping.json` file, to a more centralized and secure location (like Azure App Configuration, AWS Parameter Store, or environment variables). This will enhance maintainability and flexibility in different environments (e.g., dev, staging, prod).

8. **Health Checks and Monitoring**: Adding health checks using libraries such as `AspNetCore.HealthChecks` would provide visibility into the API's availability. Implementing monitoring and metrics (using tools like Prometheus or Application Insights) would ensure the API's reliability under load.

By implementing these improvements, the solution would become more scalable, secure, and production-ready, providing a foundation for future supplier integrations and more complex functionality.
