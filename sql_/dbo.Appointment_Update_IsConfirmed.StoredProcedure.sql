USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[Appointment_Update_IsConfirmed]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Michael Ryan Thompson>
-- Create date: <02-24-2023>
-- Description:	<Appointment Update IsConfirmed>
-- Code Reviewer: Christopher Doupis

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Appointment_Update_IsConfirmed]

	 @AppointmentId INT 
	,@ModifiedBy INT

/*
----TEST CODE----
DECLARE @AppointmentId INT = 3
		,@ModifiedBy INT = 5

SELECT *
FROM dbo.Appointments
Where Id = @AppointmentId

EXECUTE [dbo].[Appointment_Update_IsConfirmed] 
		 @AppointmentId
		,@ModifiedBy
		
SELECT *
FROM dbo.Appointments
Where Id = @AppointmentId


*/

AS

BEGIN

	DECLARE @datNow datetime2 = getutcdate()

	UPDATE [dbo].[Appointments]
		
		SET [IsConfirmed] = 1
			,[ModifiedBy] = @ModifiedBy
			,[DateModified] = @datNow
			
	WHERE Id = @AppointmentId

END
GO
