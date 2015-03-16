USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeGroup_GetByActorUser]    Script Date: 3/16/2015 1:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[EmployeeGroup_GetByActorUser] 
	@ActorUserId INT
AS
/*
Created: 20150311@Larry.Tran
Task: Get employee groups by actor user.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT eg.GroupId
		,eg.Code
		,eg.NAME
		,eg.IsDeleted AS IsDisabled
		,eg.DepartmentId
	FROM dbo.EmployeeGroup AS eg WITH (NOLOCK)
		INNER JOIN dbo.fnc_GetEmployeesByActorUser(@ActorUserId) AS e ON e.GroupId = eg.GroupId 
	WHERE eg.IsDeleted = 0
	ORDER BY eg.GroupId
END
