**Parking Management System**

**Project Overview**

The Parking Management System is a web-based application developed using ASP.NET WebForms (.aspx) with a 3-Tier Architecture and Entity Framework (Database First Approach). This system allows vehicles to enter and exit parking gates, track parking spaces, calculate charges, and apply penalties for overstays.

**Features**

Multiple entry gates with allocated parking spaces.

Parking space allocation based on the selected gate.

Hourly parking charges stored in the database.

Automatic penalty calculation for overstays.

Estimation of available parking space freeing time when the parking lot is full.

**Technologies Used**

Frontend: ASP.NET WebForms (.aspx, HTML, CSS, JavaScript)

Backend: C# (.NET Framework)

Database: SQL Server (with Entity Framework Database First)

Caching: Redis (for optimized performance, if implemented)

**Project Architecture**

This project follows a 3-Tier Architecture, which consists of:

Presentation Layer (UI Layer) - ASPX Pages

Displays user interfaces and collects inputs.

Uses HTML, CSS, and JavaScript for responsiveness.

Business Logic Layer (BLL) - C# Classes

Contains core business logic.

Handles requests from the UI and communicates with the Data Access Layer.

Data Access Layer (DAL) - Entity Framework

Interacts with the database using LINQ and Entity Framework.

Performs CRUD operations (Create, Read, Update, Delete).

**Database Schema**

**Tables**

Gates (GateId, GateName, TotalSpaces)

ParkingSpaces (SpaceId, GateId, IsOccupied)

ParkedVehicles (ParkingId, VehicleNumber, SpaceId, StartTime, EndTime, Charge)

ParkingRates (RateId, HourlyRate, PenaltyRate)

**Installation and Setup**

**Prerequisites**

Visual Studio (2019/2022)

SQL Server (LocalDB or full version)

.NET Framework installed

**Steps to Run the Project**

Clone the repository or download the source code.

Open the project in Visual Studio.

Configure the Web.config file with the correct database connection string:

<connectionStrings>
    <add name="ParkingDBEntities"
         connectionString="data source=YOUR_SERVER_NAME;initial catalog=ParkingDB;integrated security=True;"
         providerName="System.Data.EntityClient" />
</connectionStrings>

Run Update-Database in Package Manager Console (if migrations are used).

Press F5 or run the project to start the Parking Management System.

**Common Issues and Fixes**

1. No Connection String Found Error

Ensure ParkingDBEntities is correctly defined in Web.config.

2. Entity Framework Provider Not Found

Install EF provider via NuGet:

Install-Package EntityFramework.SqlServer

3. LINQ Query Issues (GetValueOrDefault() not recognized)

Use ?? false instead of .GetValueOrDefault() for nullable booleans in LINQ:

var availableSpace = _dbContext.ParkingSpaces.FirstOrDefault(s => s.GateId == gateId && !(s.IsOccupied ?? false));

4. Getting Newly Inserted Parking ID

Modify the ParkVehicle method in BLL:

var parkedVehicle = new ParkedVehicle { VehicleNumber = vehicleNumber, GateId = gateId, StartTime = DateTime.Now };
_dbContext.ParkedVehicles.Add(parkedVehicle);
_dbContext.SaveChanges();
return parkedVehicle.ParkingId; // Get the inserted ID

**Future Enhancements**

Adding user authentication and role-based access control.

Enhancing the UI with Bootstrap for better responsiveness.

Creating a reporting module for parking analytics.

**Contributors**

**Ruchila Vichare - Developer**
