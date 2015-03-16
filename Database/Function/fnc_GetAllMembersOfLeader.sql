USE [NexEPA]
GO
/****** Object:  UserDefinedFunction [dbo].[fnc_GetAllMembersOfLeader]    Script Date: 3/16/2015 1:03:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnc_GetAllMembersOfLeader] 
(	
	@ActorUserId INT
)
RETURNS @TblEmployees TABLE(EmployeeId INT,GroupId INT)
/*
Created: 20150310@Larry.Tran
Task: Get All Members Of Leader.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/  
AS
BEGIN
	;WITH AllEmployees(GroupId,EmployeeId)
	AS
	(
		SELECT eg.GroupId,e.EmployeeId
		FROM EmployeeGroup AS eg WITH(NOLOCK)
			INNER JOIN Employee AS e WITH(NOLOCK) ON eg.GroupId = e.GroupId			
		WHERE eg.IsDeleted = 0 AND e.Enabled = 1 AND e.IsDeleted = 0
			AND (eg.LeaderId = @ActorUserId 
				OR (eg.AssistantLeaderId = @ActorUserId 
					AND e.EmployeeId <> eg.LeaderId
				)
			)
		UNION ALL
		SELECT eg.GroupId,e.EmployeeId
		FROM EmployeeGroup AS eg WITH(NOLOCK)
			INNER JOIN Employee AS e WITH(NOLOCK) ON eg.GroupId = e.GroupId
			INNER JOIN AllEmployees AS g ON eg.ParentId = g.GroupId
		WHERE eg.IsDeleted = 0 AND e.Enabled = 1 AND e.IsDeleted = 0
	)
	
	INSERT INTO @TblEmployees		
	SELECT DISTINCT e.EmployeeId,e.GroupId
	FROM AllEmployees AS e
	
	RETURN
END
