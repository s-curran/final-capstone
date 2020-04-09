-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN TRANSACTION;

-- default username of 'user' and default password of 'greatwall'
INSERT INTO users
  (username,password,salt,role)
VALUES
  ('user', 'jUE98uhvS5tdIlxRsmz1W7/Qaqo=', '9CWPUTvXqQ4=', 'User');

INSERT INTO Itinerary 
	(UserID, DateOfTour, TourName, StartPoint)
VALUES 
((Select id from users where username = 'user'), '04/09/2020', 'TestTour', '343,-222');

INSERT INTO Landmark
(LandmarkID, LandmarkName, LandmarkAddress)
VALUES
('DJSFDSF', 'Someplace', '111 Someplace Ave, Ohio')

INSERT INTO Landmark
(LandmarkID, LandmarkName, LandmarkAddress)
VALUES
('EWQTE', 'Anotherplace', '222 Anotherplace Dr, Ohio')

INSERT INTO LandmarkItinerary
(LandmarkID, ItineraryID, OrderNumber)
VALUES
('DJSFDSF', (Select ItineraryId from Itinerary where TourName = 'TestTour'), 1)

INSERT INTO LandmarkItinerary
(LandmarkID, ItineraryID, OrderNumber)
VALUES
('EWQTE', (Select ItineraryId from Itinerary where TourName = 'TestTour'), 2)

COMMIT TRANSACTION;
