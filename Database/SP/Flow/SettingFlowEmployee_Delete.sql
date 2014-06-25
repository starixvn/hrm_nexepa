USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Target_Delete]    Script Date: 6/18/2014 4:48:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SettingFlowEmployee_Delete] 
	@EmployeeId INT
	,@ModifiedBy INT
	,@ModifiedDate DATETIME
	,@ErrCode INT = 0 OUTPUT
	,@ErrMsg VARCHAR(200) = '' OUTPUT
AS
/*
Created: 20140618@Quinn.Vo
Task: Delete employee flow setting.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:
• @ErrCode:
	= 0: Transaction recorded successfully.
	> 0: Specific Error.
• @ErrMsg: Error description.

*/
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.SettingEmployeeFlow WHERE EmployeeId = @EmployeeId

	IF @@ERROR <> 0
	BEGIN
		SELECT @ErrCode = 1
			,@ErrMsg = N'Delete employee flow setting failed.'
	END
	ELSE
	BEGIN
		SELECT @ErrCode = 0
			,@ErrMsg = ''
	END
END
