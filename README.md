# iFolor Student Manager

The iFolor Student Manager is a demo desktop application designed for student data management. Built using the Windows Presentation Foundation (WPF), it offers a user-friendly interface for adding, editing, and viewing basic student information. The application leverages a clean architecture, separating concerns into core business logic, data access infrastructure, and the presentation layer.

## Application Structure

The application is structured into three main projects, each with a distinct responsibility to ensure maintainability and scalability:

### Core Project

This project is at the heart of the application, containing:

- **Domain Models**: Represents the essential entities, such as Student, Course, etc., encapsulating the business data and rules.
- **Business Logic**: Houses the application's workflows, validation rules, and business processes, dictating the application behavior.
- **Interfaces**: Defines contracts for services and repositories to achieve inversion of control and dependency injection, facilitating a loosely coupled design.

### Infrastructure Project

Implements the Core project interfaces and handles interactions with external resources. It focuses on:

- **Data Access**: Concrete implementations for managing XML file storage, performing CRUD operations on student data.
- **External Services Integration**: If needed, this would include logic for integrating with external web services or APIs.

### Windows Project

Contains the WPF-based graphical user interface, implementing:

- **Views**: The visual elements and components through which users interact with the application.
- **View Models**: Implements the MVVM pattern, binding UI elements to the application data and handling user commands.
- **User Interaction Logic**: Manages user inputs, navigation, and application responses, ensuring a responsive and intuitive user experience.
- **Data Binding**: Dynamically links UI elements to the data models and view models, ensuring that UI updates reflect underlying data changes.

## Getting Started

To get started with the iFolor Student Manager, follow these steps to set up your environment, build the application, and run it.

### Prerequisites

- Visual Studio 2022 or later
- .NET 8 SDK