use VenueFinder;

CREATE TABLE [dbo].[VenueType](
	[VenueTypeId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[Description] [varchar](500) NOT NULL);

CREATE TABLE [dbo].[Venue](
	[VenueId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Summary] [varchar](max) NOT NULL,
	[Testimonial] [varchar](500) NULL,
	[TestimonialContactName] [varchar](500) NULL,
	[TestimonialContactOrganisation] [varchar](500) NULL,
	[TestimonialContactEmail] [varchar](500) NULL,
	[MUrl] [varchar](250) NOT NULL,
	[VenueTypeId] [uniqueidentifier] FOREIGN KEY REFERENCES [VenueType]([VenueTypeId]) NOT NULL );

CREATE TABLE [dbo].[VenueImage](
	[VenueImageId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[VenueId] [uniqueidentifier] FOREIGN KEY REFERENCES Venue([VenueId]) NOT NULL,
	[Base64VenueImageString] [varchar](max) NOT NULL );

CREATE TABLE [dbo].[SpaceType](
	[SpaceTypeId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[Description] [varchar](500) NOT NULL);

CREATE TABLE [dbo].[Space](
	[SpaceId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[VenueId] [uniqueidentifier] FOREIGN KEY REFERENCES Venue([VenueId]) NOT NULL,
	[MaxCapacity] [int] NOT NULL,
	[SpaceTypeId] [uniqueidentifier] FOREIGN KEY REFERENCES SpaceType([SpaceTypeId]) NOT NULL );
	
CREATE TABLE [dbo].[SpaceImage](
	[SpaceImageId] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY NOT NULL,
	[SpaceId] [uniqueidentifier] FOREIGN KEY REFERENCES Space([SpaceId]) NOT NULL,
	[Base64SpaceImageString] [varchar](max) NULL );


SELECT S.SpaceId, S.VenueId, S.MaxCapacity, 
ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, 
SI.SpaceImageId, SI.Base64SpaceImageString
FROM [VenueFinder].[dbo].[Space] S 
JOIN [VenueFinder].[dbo].SpaceType ST ON ST.SpaceTypeId = S.SpaceTypeId 
LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId
WHERE S.VenueId = '048944B6-8A4C-48E3-84AB-B63E495067DB';

select V.VenueId, V.Title, V.Description, V.MUrl, V.Summary, 
V.Testimonial, V.TestimonialContactEmail, V.TestimonialContactName, V.TestimonialContactOrganisation,
VI.VenueImageId, VI.Base64VenueImageString,
VT.VenueTypeId, VT.Description as VenueTypeDescription 
from [VenueFinder].[dbo].Venue V
left outer join [VenueFinder].[dbo].VenueImage VI 
on VI.VenueId = V.VenueId
left outer join [VenueFinder].[dbo].VenueType VT 
on VT.VenueTypeId = V.VenueTypeId 
WHERE V.VenueId = '60C99C26-315F-491C-9971-3AEB0776A36B';


insert into VenueType (Description) values ('Example description');
insert into SpaceType (Description) values ('Example space type description');