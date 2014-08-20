USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Function_GetByRole]    Script Date: 8/19/2014 5:40:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Function_GetByRole] 
	@RoleId INT
AS
/*
Created: 20140605@Quinn.Vo
Task: Get all functions by role
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT fn.[FunctionId]
		,fn.[FunctionKey]
		,fn.[Name]
		,fn.[ParentFunctionId]
		,fn.IsReadOnly
		,rf.IsRead
		,rf.IsHost
	FROM dbo.[Function] AS fn WITH (NOLOCK)
		LEFT JOIN (
		SELECT FunctionId
			,IsRead
			,IsHost
		FROM dbo.RoleFunction WITH (NOLOCK)
		WHERE RoleId = @RoleId
		) AS rf ON rf.FunctionId = fn.FunctionId
	WHERE IsDisabled = 0
END
