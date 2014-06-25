USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[SettingFlowDepartment_Get]    Script Date: 6/18/2014 4:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SettingFlowEmployee_Get] 
	@EmployeeId INT
AS
/*
Created: 20140618@Quinn.Vo
Task: Get employee flow setting.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	SELECT ef.ProcessId
		,ef.[Priority]
		,ef.EvaluatorId
		,e.Code
		,e.NickName
		,e.FullName
		,e.Email
	FROM dbo.SettingEmployeeFlow AS ef WITH (NOLOCK)
		LEFT JOIN dbo.Employee AS e WITH (NOLOCK) ON ef.EvaluatorId = e.EmployeeId
	WHERE ef.EmployeeId = @EmployeeId
END
