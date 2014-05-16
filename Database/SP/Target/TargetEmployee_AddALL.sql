USE [NexEPA]
GO

/****** Object:  StoredProcedure [dbo].[TargetEmployee_Add]    Script Date: 4/26/2014 3:19:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[TargetEmployee_AddALL] 
	@TargetId INT
	,@ProjectId INT
	,@AddedBy INT
	,@AddedDate DATETIME
	,@ErrCode INT = 0 OUTPUT
	,@ErrMsg VARCHAR(200) = '' OUTPUT
AS
/*
Created: 20140426@Quinn.Vo
Task: Add Employee KPI into Target.
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

	INSERT dbo.TargetEmployee (
		TargetId
		,EmployeeId
		,AddedBy
		,AddedDate
		)
	SELECT @TargetId, EmployeeId, @AddedBy, @AddedDate
	FROM dbo.ProjectMember WITH (NOLOCK)
	WHERE ProjectId = @ProjectId

	-- EXECUTE dbo.TargetEmployeeKPI_Add @TargetId, @EmployeeId, @ProjectRoleId, @ErrCode, @ErrMsg

	-- EXECUTE dbo.TargetEmployeeProcess_Add @TargetId, @EmployeeId, @AddedBy, @AddedDate, @ErrCode, @ErrMsg

	DECLARE @StartDate DATETIME
	DECLARE @EndDate DATETIME
	DECLARE @EvaluatorId INT
	DECLARE @ProcessGroupId INT
	DECLARE @ProcessId INT
	DECLARE @ProcessDays INT
	DECLARE @IsSelfEvaluated BIT
	DECLARE @IsEvaluatedByPM BIT
	DECLARE @IsEveluatedByLeader BIT
	DECLARE @UpperLevel TINYINT

	SELECT @ProcessGroupId = tt.ProcessGroupId
		,@StartDate = tg.StartDate
		,@EndDate = tg.EndDate
	FROM dbo.[Target] AS tg WITH (NOLOCK)
	INNER JOIN dbo.TargetType AS tt WITH (NOLOCK) ON tg.TargetTypeId = tt.TargetTypeId
	WHERE TargetId = @TargetId

	SELECT TOP 1
		@ProcessId = ProcessId
		,@IsSelfEvaluated = IsSelfEvaluated 
		,@IsEvaluatedByPM = IsEvaluatedByPM
		,@IsEveluatedByLeader = IsEveluatedByLeader
		,@UpperLevel = UpperLevel
	FROM dbo.Process WITH (NOLOCK)
	WHERE ProcessGroupId = @ProcessGroupId
	

	IF(@IsEvaluatedByPM = 1)
	BEGIN
		SELECT @EvaluatorId = ProjectManager 
		FROM dbo.[Target] AS tg WITH (NOLOCK)
		INNER JOIN dbo.Project AS pr WITH (NOLOCK) ON tg.ProjectId = pr.ProjectId
		WHERE tg.TargetId = @TargetId
	END

	INSERT INTO dbo.TargetEmployeeProcess( 
		[TargetId]
      ,[EmployeeId]
      ,[ProcessId]
      ,[EvaluatorId]
      ,[StartDate]
      ,[EndDate]
      ,[ModifiedBy]
      ,[ModifiedDate])
	  SELECT @TargetId, EmployeeId, @ProcessId, @EvaluatorId, @StartDate, @EndDate, @AddedBy, @AddedDate
	  FROM dbo.ProjectMember WITH (NOLOCK)
	  WHERE ProjectId = @ProjectId

	IF @@ERROR <> 0
	BEGIN
		SELECT @ErrCode = 1
			,@ErrMsg = N'Add Employee KPI failed'
	END
	ELSE
	BEGIN
		SELECT @ErrCode = 0
			,@ErrMsg = ''
	END
END
