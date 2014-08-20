USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Function_GetAll]    Script Date: 8/19/2014 5:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Function_GetAll]
AS
/*
Created: 20140604@Quinn.Vo
Task: Get all functions
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[FunctionId]
		,[FunctionKey]
		,[Name]
		,[ParentFunctionId]
		,IsReadOnly
	FROM dbo.[Function] WITH (NOLOCK)
	WHERE IsDisabled = 0
END
