-- =========================================
-- Create table template
-- =========================================
USE VenueFinder

CREATE TABLE Venue
(
	VenueId uniqueidentifier DEFAULT NEWID() PRIMARY KEY NOT NULL, 
	Description VARCHAR(500) NOT NULL,
	MUrl VARCHAR(250) NOT NULL
)

CREATE TABLE SpaceType
(
	SpaceTypeId uniqueidentifier DEFAULT NEWID() PRIMARY KEY NOT NULL,
	Description VARCHAR(500) NOT NULL	
)

CREATE TABLE Space
(
	SpaceId uniqueidentifier DEFAULT NEWID() PRIMARY KEY NOT NULL, 
	VenueId uniqueidentifier FOREIGN KEY REFERENCES Venue(VenueId),
	MaxCapacity INT NOT NULL,
	SpaceType uniqueidentifier FOREIGN KEY REFERENCES SpaceType(SpaceTypeId)
)

CREATE TABLE Booking
(
	BookingId uniqueidentifier DEFAULT NEWID() PRIMARY KEY NOT NULL, 
	StartDate date NOT NULL,
	EndDate date NOT NULL
)

CREATE TABLE SpaceBooking
(
	BookingId uniqueidentifier DEFAULT NEWID() PRIMARY KEY NOT NULL, 
	SpaceId uniqueidentifier FOREIGN KEY REFERENCES Space(SpaceId),
	PeopleBooked INT NOT NULL
)


--drop table VenueFinder.dbo.Booking;
--drop table VenueFinder.dbo.SpaceBooking;
--drop table VenueFinder.dbo.Space;
--drop table VenueFinder.dbo.SpaceType;
--drop table VenueFinder.dbo.Venue;