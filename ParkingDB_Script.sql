CREATE TABLE Gates (
    GateId INT PRIMARY KEY IDENTITY(1,1),
    GateName NVARCHAR(50),
    TotalSpaces INT
);

CREATE TABLE ParkingSpaces (
    SpaceId INT PRIMARY KEY IDENTITY(1,1),
    GateId INT FOREIGN KEY REFERENCES Gates(GateId),
    IsOccupied BIT
);

CREATE TABLE ParkingRates (
    RateId INT PRIMARY KEY IDENTITY(1,1),
    HourlyRate DECIMAL(10, 2),
    PenaltyRate DECIMAL(10, 2)
);

CREATE TABLE ParkedVehicles (
    ParkingId INT PRIMARY KEY IDENTITY(1,1),
    VehicleNumber NVARCHAR(20),
    SpaceId INT FOREIGN KEY REFERENCES ParkingSpaces(SpaceId),
    StartTime DATETIME,
    EndTime DATETIME NULL
);

create database ParkingDB
use ParkingDB