USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[SettingFlowDepartment_Set]    Script Date: 6/18/2014 3:26:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SettingFlowEmployee_Set] 
	@EmployeeId INT
	,@FlowDetails VARCHAR(5000)
	,@ModifiedBy INT
	,@ModifiedDate DATETIME
	,@ErrCode INT = 0 OUTPUT
	,@ErrMsg VARCHAR(200) = '' OUTPUT
AS
/*
Created: 20140618@Quinn.Vo
Task: Save employee flow setting.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:
• @FlowDetails: N'<ROOT>
						 <Process
							ProcessId="1"
							EvaluatorId="125">
						  </Process>
						</ROOT>'
• @ErrCode:
	= 0: Transaction recorded successfully.
	> 0: Specific Error.
• @ErrMsg: Error description.
*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @EmployeeFlows TABLE (
		ProcessId INT
		,EvaluatorId INT
		)

	BEGIN TRY
		DECLARE @DocHandler INT

		EXEC sp_xml_preparedocument @DocHandler OUTPUT
			,@FlowDetails

		INSERT INTO @EmployeeFlows (
			ProcessId
			,EvaluatorId
			)
		SELECT ProcessId
			,EvaluatorId
		FROM OPENXML(@DocHandler, '/ROOT/Process', 1) WITH (ProcessId INT, EvaluatorId INT) AS Data

		EXEC sp_xml_removedocument @DocHandler
	END TRY

	BEGIN CATCH
		SELECT @ErrCode = 1
			,@ErrMsg = N'Save employee flow setting failed. Paramter invalid.'

		RETURN
	END CATCH

	BEGIN TRY
		BEGIN TRANSACTION;

		DELETE
		FROM dbo.SettingEmployeeFlow
		WHERE EmployeeId = @EmployeeId

		INSERT INTO dbo.SettingEmployeeFlow (
			EmployeeId
			,ProcessId
			,EvaluatorId
			,[Priority]
			,ModifiedBy
			,ModifiedDate
			)
		SELECT @EmployeeId
			,ef.ProcessId
			,ef.EvaluatorId
			,pc.[Priority]
			,@ModifiedBy
			,@ModifiedDate
		FROM @EmployeeFlows AS ef
			INNER JOIN dbo.Process AS pc ON ef.ProcessId = pc.ProcessId

		COMMIT TRANSACTION;

		SELECT @ErrCode = 0
			,@ErrMsg = ''
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT @ErrCode = 1
			,@ErrMsg = N'Save employee flow setting failed.'
	END CATCH
END
