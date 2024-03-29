USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPoints_SelectByUserId_RunningTotals]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPoints Select By UserId Paginated proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[PatriotPoints_SelectByUserId_RunningTotals]
				
			@UserId INT

as

/*----------Test Code-------------------

	DECLARE  @UserId int = 73
	
	EXECUTE [dbo].[PatriotPoints_SelectByUserId_RunningTotals] 
												@UserId
														  
	SELECT *
	From dbo.PatriotPoints

	SELECT *
	From dbo.PatriotPointsSource

	 
	
	
*/
	
BEGIN

	SELECT  TotalLifeTimePoints = (SELECT SUM (CASE WHEN ps.[PointsAwarded] >= 0 then ps.PointsAwarded else 0 end)
									FROM [dbo].[PatriotPoints] as pp
									INNER JOIN [dbo].[PatriotPointsSource] as ps
									ON pp.[SourceId] = ps.[Id]
									WHERE pp.[UserId] = @UserId)
			,TotalPointsRedeemed = (SELECT SUM (CASE WHEN ps.[PointsAwarded] < 0 then ps.PointsAwarded else  0 end)
									FROM [dbo].[PatriotPoints] as pp
									INNER JOIN [dbo].[PatriotPointsSource] as ps
									ON pp.[SourceId] = ps.[Id]
									WHERE pp.[UserId] = @UserId)
			,TotalPointsAvailable = (SELECT SUM (ps.[PointsAwarded])
									FROM [dbo].[PatriotPoints] as pp
									INNER JOIN [dbo].[PatriotPointsSource] as ps
									ON pp.[SourceId] = ps.[Id]
									WHERE pp.[UserId] = @UserId )
			

	 FROM [dbo].[PatriotPoints] as pp
	 INNER JOIN [dbo].[PatriotPointsSource] as ps
	 ON pp.[SourceId] = ps.[Id]
	 WHERE pp.[UserId] = @UserId
	 Group By pp.[UserId]

	END
GO
