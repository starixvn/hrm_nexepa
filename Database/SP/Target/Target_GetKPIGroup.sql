USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Target_GetKPIGroup]    Script Date: 5/17/2014 3:37:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Target_GetKPIGroup] 
	@TargetId INT
AS
/*
Created: 20140517@Josh.Phan
Task: Get all KPI Group by TargetId
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT gr.KPIGroupId, gr.Name
	FROM dbo.TargetEmployeeKPI AS tek WITH (NOLOCK)
		inner join dbo.KPIGroup AS gr WITH (NOLOCK) on tek.KPIGroupId = gr.KPIGroupId
	WHERE tek.TargetId = @TargetId
END
