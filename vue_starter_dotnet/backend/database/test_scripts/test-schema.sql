
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='DemoTestDB')
DROP DATABASE DemoTestDB;
GO

-- Create a new DemoDB Database
CREATE DATABASE DemoTestDB;
GO

-- Switch to the DemoDB Database
USE DemoTestDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);


create table Itinerary (
	ItineraryID int Primary Key Identity not null,
	UserID int not null,
	DateOfTour date not null Default getdate(),
	TourName nvarchar(100) not null,
	StartPoint nvarchar(150) not null,
	Constraint fk_UserID foreign key (UserID) references users(id)
)

create table Landmark (
	LandmarkID nvarchar(150) Primary Key not null,
	LandmarkName nvarchar(150) not null,
	LandmarkAddress nvarchar(150) not null,
)

create table LandmarkItinerary (
	LandmarkID nvarchar(150) not null,
	ItineraryID int not null,
	OrderNumber int not null,
	Constraint fk_LandmarkID foreign key(LandmarkID) references Landmark(LandmarkID),
	Constraint fk_ItineraryID foreign key (ItineraryID) references Itinerary(ItineraryID),
	primary key (LandmarkID, ItineraryID),
)

COMMIT TRANSACTION;