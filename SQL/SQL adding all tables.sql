USE CarpoolApp

Select * FROM Users

CREATE TABLE Users(
UserID int IDENTITY(0,1) PRIMARY KEY,
Password NVARCHAR(50) NOT NULL,
Firstname VARCHAR(20) NOT NULL,
Lastname VARCHAR(20) NOT NULL,
CanDrive BIT NOT NULL,
)

CREATE TABLE Carpools(
CarpoolID int IDENTITY(0,1) PRIMARY KEY,
CarpoolDriverID int FOREIGN KEY REFERENCES Users(UserID),
Password NVARCHAR(50) NOT NULL,
Origin VARCHAR(50) NOT NULL,
Destination VARCHAR(50) NOT NULL,
FreeSpaces int NOT NULL,
DepartmentTime SMALLDATETIME NOT NULL,
)

CREATE TABLE CarpoolPassengers(
CarpoolID int FOREIGN KEY REFERENCES Carpools(CarpoolID),
PassengerID int FOREIGN KEY REFERENCES Users(UserID),
PRIMARY KEY(CarpoolID,PassengerID)
)
 
Drop TABLE CarpoolPassengers
DROP TABLE Carpools
DROP TABLE Users
