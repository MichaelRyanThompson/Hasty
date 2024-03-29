USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[Appointment_SelectById]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Michael Ryan Thompson>
-- Create date: <02-24-2023>
-- Description: Appointment SelectById
-- Code Reviewer: Christopher Doupis

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Appointment_SelectById]
	
			@Id INT

as
/*----------Test Code-------------------

	DECLARE @Id INT = 5
	
	EXECUTE [dbo].[Appointment_SelectById] @Id
	
	
*/

BEGIN
	
	SELECT   a.[Id]
			,a.[Phone]
			,a.[StartDateTime]
			,a.[Time]
			,a.[IsConfirmed]
			,a.[IsCanceled]
			,a.[DateCreated]
			,a.[DateModified]
			,u.[Id] as CreatedById
			,u.[FirstName] as createdByFirstName
			,u.[LastName] as CreatedbyLastName
			,u.[Mi] as CreatedByMi
			,u.[AvatarUrl] as CreatedByAvatarUrl
			,u2.[Id] as ModifiedById
			,u2.[FirstName] as ModifiedByFirstName
			,u2.[LastName] as ModifiedByLastName
			,u2.[Mi] as ModifiedByMi
			,u2.[AvatarUrl] as ModifiedByAvatarUrl
			,l.[Id] as ListingId
			,l.[InternalName]
			,l.[Title]
			,l.[ShortDescription]
			,l.[Description]
			,l.[BedRooms]
			,l.[Baths]
			,hts.[Id] AS 'HousingType.Id'
			,hts.[Name] AS 'HousingType.Name'
			,ats.[Id] AS 'AccessType.Id'
			,ats.[Name] AS 'AccessType.Name'
			,ListingServices = (
							SELECT avs.Id,
									avs.Name
							FROM dbo.ListingServices AS ls
							INNER JOIN dbo.AvailableServices AS avs
								ON ls.ServiceId = avs.Id
							WHERE l.Id = ls.ListingId
							FOR JSON AUTO
						),
								   
						ListingAmenities = (
							SELECT a.Id,
									a.Name
							FROM dbo.ListingAmenities AS lga
								INNER JOIN dbo.Amenities AS a
									ON lga.AmenityId = a.Id
							WHERE l.Id = lga.ListingId
							FOR JSON AUTO
						)
								   
			,l.GuestCapacity
			,l.CostPerNight
			,l.CostPerWeek
			,l.CheckInTime
			,l.CheckOutTime
			,l.DaysAvailable
			,l.HasVerifiedOwnerShip
			,l.IsActive
			,l.CreatedBy
			,l.DateCreated
			
			

	 FROM [dbo].[Appointments] as a
	 INNER JOIN [dbo].[Users] as u
	 ON a.[CreatedBy] = u.[Id]
	 INNER JOIN [dbo].[Users] as u2
	 ON a.[ModifiedBy] = u2.[Id]
	 INNER JOIN [dbo].[Listings] as l
	 ON a.[ListingId] = l.[Id]
	 INNER JOIN dbo.AccessTypes AS ats
	 ON l.AccessTypeId = ats.Id
	 INNER JOIN dbo.HousingTypes AS hts
	 ON l.HousingTypeId = hts.Id
	
  
	 WHERE a.Id = @Id

END
GO
