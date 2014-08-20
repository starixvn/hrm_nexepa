USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[SystemRole_GetAll]    Script Date: 8/19/2014 5:37:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SystemRole_GetAll]
AS
/*
Created: 20140318@Quinn.Vo
Task: Get all role system.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT RoleId, Name
	FROM dbo.SystemRole WITH (NOLOCK)
END
