Begin Transaction

create table Itinerary (
	ItineraryID int Primary Key Identity not null,
	UserID int not null,
	DateOfTour date not null Default getdate(),
	TourName nvarchar(100) not null,
	StartPoint nvarchar(150) not null,
	Constraint fk_UserID foreign key (UserID) references users(id)
)

create table Landmark (
	LandmarkID int Primary Key not null,
	LandmarkName nvarchar(150) not null,
	LandmarkAddress nvarchar(150) not null,
)

create table LandmarkItinerary (
	LandmarkID int not null,
	ItineraryID int not null,
	OrderNumber int not null,
	Constraint fk_LandmarkID foreign key(LandmarkID) references Landmark(LandmarkID),
	Constraint fk_ItineraryID foreign key (ItineraryID) references Itinerary(ItineraryID),
	primary key (LandmarkID, ItineraryID),
)

Commit Transaction