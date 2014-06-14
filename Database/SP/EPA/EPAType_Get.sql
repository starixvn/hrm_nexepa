USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[EPAType_Get]    Script Date: 6/14/2014 10:26:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EPAType_Get] 
	@EPATypeId INT = NULL
AS
/*
Created: 20140614@Josh.Phan
Task: Implement Target Employee Setting page.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:
*/
BEGIN
	SET NOCOUNT ON;

	SELECT epa.[EPATypeId]
		,epa.[Name]
		,epa.[TargetTypeId]
		,epa.[ScheduleRunType]

	FROM dbo.EPAType AS epa WITH (NOLOCK)

	WHERE @EPATypeId IS NULL
		OR epa.EPATypeId = @EPATypeId	  
		 
END
