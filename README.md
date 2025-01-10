# CatAdoption

CatAdoption is a full-stack mobile application designed to facilitate cat adoptions. The project consists of a .NET MAUI mobile application for Android and a backend powered by ASP.NET Web API with Entity Framework Core.

## Features

### Mobile Application

- **Login/Register**: Users must create an account to access app features.
- **Adopt a Cat**: Browse available cats and adopt one.
- **Favorite Cats**: Mark cats as favorites for quick access.
- **View Adoptions**: Track adopted cats.
- **Change Password**: Update account credentials.
- **Notifications**: Real-time alerts via SignalR when:
  - Multiple users are viewing the same cat.
  - A favorited cat gets adopted.

### Backend

- Static test data for cats.
- User authentication and management.
- Real-time communication using SignalR.
- Database operations via Repository pattern.

## Project Structure

The project is divided into the following components:

- **CatAdoption.API**: Contains ASP.NET Web API endpoints for user and cat management.
- **CatAdoption.EntityFramework**: Implements database context and repositories using Entity Framework Core.
- **CatAdoption.MAUI**: The mobile application built with .NET MAUI and MVVM architecture.
- **CatAdoption.Shared**: Contains shared data models and utilities.

## Technologies Used

### Frontend

- **.NET MAUI**: Cross-platform mobile development.
- **MVVM Architecture**: Clean separation of concerns.

### Backend

- **ASP.NET Web API**: RESTful API for data exchange.
- **Entity Framework Core**: Database management.
- **SignalR**: Real-time communication.
- **Refit**: Simplified API calls in the mobile app.

### Design Patterns

- **Facade Pattern**: Simplifies complex subsystems.
- **Mediator Pattern**: Manages communication between objects.
- **Repository Pattern**: Abstracts database operations.
- **Dependency Injection**: Manages service lifetimes and dependencies.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later
- Android emulator or physical device for testing

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/CatAdoption.git
cd CatAdoption
```

### Backend Setup

I am using local MSSQL Server, you will need to install it, create a database, change the connection string in appsettings.json to yours and apply migrations

1. Navigate to the `CatAdoption.API` directory.
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the API:
   ```bash
   dotnet run
   ```

### Mobile Application Setup

1. Open the solution in Visual Studio.
2. Set the startup project to `CatAdoption.MAUI`.
3. Deploy the app to an Android emulator or device.

## Usage

1. Register a new account or log in with an existing one.
2. Browse all cats or recomended cats.
3. Mark cats as favorites or adopt them.
4. View your adopted cats and manage your account settings.

## Future Improvements

- Support for iOS and other platforms.
- Integration with a live database.
- Advanced search and filtering options for cats.
- Enhanced user profiles and preferences.



