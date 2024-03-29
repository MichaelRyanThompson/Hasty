USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[Appointment_Insert]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Michael Ryan Thompson>
-- Create date: <02-24-2023>
-- Description:	<Appointment Insert>
-- Code Reviewer: Christopher Doupis

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Appointment_Insert]
	 @ListingId INT
	,@Phone NVARCHAR(50)
	,@StartDateTime DATETIME2(7)
	,@Time TIME(7)
	,@UserId INT
	,@AppointmentId INT OUTPUT

/*
----TEST CODE----
DECLARE @AppointmentId int = 0;

DECLARE  @ListingId INT = 53
		,@Phone NVARCHAR(50) = 'DELETE ME'
		,@StartDateTime DATETIME2(7) = '2023-04-10T10:00:00'
		,@Time TIME(7) = '12:00:00'
		,@UserId INT = 74

EXECUTE [dbo].[Appointment_Insert]
		 @ListingId
		,@Phone
		,@StartDateTime
		,@Time
		,@UserId
		,@AppointmentId OUTPUT


SELECT *
FROM dbo.Appointments
Where Id = @AppointmentId


*/

AS

BEGIN

	INSERT INTO [dbo].[Appointments] (
		 [ListingId]
		,[Phone]
		,[StartDateTime]
		,[Time]
		,[CreatedBy]
		,[ModifiedBy]
	)

	VALUES (
		 @ListingId
		,@Phone
		,@StartDateTime
		,@Time
		,@UserId
		,@UserId
	)

	SET @AppointmentId = SCOPE_IDENTITY()
END
GO
