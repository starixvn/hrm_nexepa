/****** Object:  StoredProcedure [dbo].[Permission_IsOnlyMember]    Script Date: 2/13/2015 9:09:23 AM ******/
IF OBJECT_ID('[dbo].[Permission_IsOnlyMember]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[Permission_IsOnlyMember]
END
GO

/****** Object:  StoredProcedure [dbo].[Permission_IsOnlyMember]    Script Date: 2/13/2015 9:09:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Permission_IsOnlyMember]
	@EmployeeId INT,
	@IsOnlyMember BIT OUT
AS
/*
Created: 20150211@Larry.Tran
Task: Check if employee has only Member role.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON

	DECLARE @RoleCount INT

	SELECT @RoleCount = COUNT(*)
	FROM dbo.EmployeeRole AS er WITH (NOLOCK)
		INNER JOIN dbo.SystemRole AS sr WITH (NOLOCK) ON er.RoleId = sr.RoleId
	WHERE er.EmployeeId = @EmployeeId

	IF ISNULL(@RoleCount,1) = 1
	BEGIN
		SET @IsOnlyMember = 1
	END
	ELSE
	BEGIN
		SET @IsOnlyMember = 0
	END
END
GO


