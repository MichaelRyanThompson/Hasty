USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[AppointmentConfirmation]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AppointmentConfirmation]
	 @ListingId INT
	,@UserId INT
	

/*
----TEST CODE----
DECLARE @ListingId INT = 53;
DECLARE @UserId INT = 73;


EXECUTE dbo.AppointmentConfirmation
		 @ListingId
		,@UserId
		
*/

AS

BEGIN

DECLARE @ListingOwnerId INT = 0;

EXECUTE [dbo].[Users_SelectByCurrentUser] @UserId

EXECUTE dbo.Listings_Select_ByIdV2 @ListingId


		SELECT @ListingOwnerId =  CreatedBy

	FROM dbo.Listings
	WHERE Id = @ListingId

EXECUTE [dbo].[Users_SelectByCurrentUser] @ListingOwnerId

		
END
GO
