# Event Management System

## Description

The Event Management System is a web application built with ASP.NET Core that allows users to create, manage, and register for events. It provides a platform for event organizers to list their events and for attendees to browse and sign up for events that interest them.

Key features include:
- User authentication and authorization
- Event creation and management
- Event registration for attendees
- PDF ticket generation for registered events
- Admin panel for overall system management

## Technologies Used

- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- Bootstrap 5
- HTML5 & CSS3
- JavaScript
- iTextSharp (for PDF generation)

## Prerequisites

Before you begin, ensure you have met the following requirements:
- .NET 6.0 SDK
- SQL Server
- Visual Studio 2022 or Visual Studio Code

## Installation

To install the Event Management System, follow these steps:

1. Clone the repository
   ```
   git clone https://github.com/yourusername/EventManagementSystem.git
   ```

2. Navigate to the project directory
   ```
   cd EventManagementSystem
   ```

3. Restore the NuGet packages
   ```
   dotnet restore
   ```

4. Update the connection string in `appsettings.json` to point to your SQL Server instance

5. Apply the database migrations
   ```
   dotnet ef database update
   ```

6. Run the application
   ```
   dotnet run
   ```

## Usage

After installation, you can use the system as follows:

1. Register a new account or log in with existing credentials
2. Browse available events on the Events page
3. Click on an event to view details and register
4. View your registered events in the My Registrations section
5. Admin users can create, edit, and delete events

## Contributing

Contributions to the Event Management System are welcome. Please follow these steps to contribute:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Contact

Your Name - karthikmudaliar20@gmail.com

Project Link: [https://github.com/yourusername/EventManagementSystem](https://github.com/yourusername/EventManagementSystem)

## Acknowledgements

- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Bootstrap](https://getbootstrap.com)
- [iTextSharp](https://github.com/itext/itextsharp)
