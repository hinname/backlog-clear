## About the project

The Web API is **RESTful** and follows best practices for API design, making it easy to integrate with other applications and services.  
It also uses **DDD (Domain-Driven Design)** principles to ensure the code is well-organized and maintainable.

The API utilizes NuGet packages such as **FluentValidation** for validating incoming requests, **AutoMapper** for mapping between different object types, **FluentAssertions** for writing unit tests, and **EntityFrameworkCore** for database access.

#### Features

- **CRUD** game operations: Create, Read, Update, and Delete games.
- **Domain-Driven Design**: The API is designed using DDD principles, ensuring a clean separation of concerns and a well-structured codebase.
- **Unit tests**: The API is tested using **xUnit** and **FluentAssertions** to ensure the code works as expected.
- **Reports**: The API provides detailed reports in **PDF** and **Excel** formats, making it easy for users to share and analyze their game data.

## Getting Started

To run this project locally, follow these steps.

### Requirements
- Visual Studio 2022+, Visual Studio Code, or Rider (IDE)
- Windows 10+ or Linux/macOS with .NET SDK 8 installed ([Click here to install](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0))
- MySQL Server or MariaDB

### Installation
1. Clone this repository:
    ```sh
    git clone https://github.com/hinname/backlog-clear.git
    ```
2. Update the `appsettings.Development.json` file with your configuration.
3. Run the API and you're good to go!



