USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Employee_GetByTeamProject]    Script Date: 3/16/2015 1:15:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC Employee_GetByTeamProject 169
ALTER PROCEDURE [dbo].[Employee_GetByTeamProject] 
	@ActorUserId INT	
	,@GroupId INT = NULL
	,@ProjectId INT = NULL
AS
/*
Created: 20150225@Larry.Tran
Task: Get Employee by Team and Project.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @IsSystemAdmin BIT
	DECLARE @IsPM BIT
	DECLARE @IsLeader BIT

	SELECT @IsSystemAdmin = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 1
	SELECT @IsPM = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 3
	SELECT @IsLeader = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 4

	IF OBJECT_ID('#TblEmployeeByTeam') IS NOT NULL
	BEGIN
		DROP TABLE #TblEmployeeByTeam
	END

	CREATE TABLE #TblEmployeeByTeam (
		EmployeeId INT
	)

	IF OBJECT_ID('#TblEmployeeByProject') IS NOT NULL
	BEGIN
		DROP TABLE #TblEmployeeByProject
	END

	CREATE TABLE #TblEmployeeByProject (
		EmployeeId INT
	)	
	--Collect data
	IF @IsSystemAdmin = 1
	BEGIN
		INSERT INTO #TblEmployeeByTeam
		SELECT ee.[EmployeeId]
		FROM dbo.Employee AS ee WITH (NOLOCK)
			INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON ee.GroupId = eg.GroupId
		WHERE @GroupId IS NULL OR ee.GroupId = @GroupId

		INSERT INTO #TblEmployeeByProject
		SELECT DISTINCT ee.[EmployeeId]
		FROM dbo.Employee AS ee WITH (NOLOCK)
		INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON ee.GroupId = eg.GroupId
		INNER JOIN ProjectMember AS m WITH (NOLOCK) ON ee.EmployeeId = m.EmployeeId AND m.IsDisabled = 0
		INNER JOIN Project AS p WITH (NOLOCK) ON p.ProjectId = m.ProjectId
		WHERE @ProjectId IS NULL OR p.ProjectId = @ProjectId
	END
	ELSE
	BEGIN
		IF @IsPM = 1
		BEGIN
			INSERT INTO #TblEmployeeByProject
			--Actor is PM
			SELECT DISTINCT ee.[EmployeeId]
			FROM dbo.Employee AS ee WITH (NOLOCK)
			INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON ee.GroupId = eg.GroupId
			INNER JOIN ProjectMember AS m WITH (NOLOCK) ON ee.EmployeeId = m.EmployeeId AND m.IsDisabled = 0
			INNER JOIN Project AS p WITH (NOLOCK) ON p.ProjectId = m.ProjectId
			WHERE p.ProjectManager = @ActorUserId
				AND (@ProjectId IS NULL OR p.ProjectId = @ProjectId)
				AND (@GroupId IS NULL OR ee.GroupId = @GroupId)
			UNION
			--Actor is Evaluator
			SELECT DISTINCT ee.[EmployeeId]
			FROM dbo.Employee AS ee WITH (NOLOCK)
			INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON ee.GroupId = eg.GroupId
			INNER JOIN EPA AS epa WITH (NOLOCK) ON epa.EmployeeId = ee.EmployeeId
			INNER JOIN EPAProcess AS ep WITH (NOLOCK) ON epa.EPAId = ep.EPAId
			INNER JOIN Target AS tg WITH (NOLOCK) ON tg.TargetId = epa.TargetId
			INNER JOIN TargetProject AS tp WITH (NOLOCK) ON tp.TargetId = tg.TargetId
			WHERE ep.EvaluatorId = @ActorUserId
				AND (@ProjectId IS NULL OR tp.ProjectId = @ProjectId)
				AND (@GroupId IS NULL OR ee.GroupId = @GroupId)				 
		END
		
		IF @IsLeader = 1
		BEGIN			
			INSERT #TblEmployeeByTeam(EmployeeId)
			SELECT DISTINCT e.EmployeeId
			FROM dbo.fnc_GetAllMembersOfLeader(@ActorUserId) AS e			
			WHERE @GroupId IS NULL OR e.GroupId = @GroupId						 
		END
	END	
	
	--Handle to get employee
	IF (@IsSystemAdmin = 1 OR (@IsLeader = 1 AND ISNULL(@IsPM,0) <> 1))
	BEGIN
		IF @ProjectId IS NULL
		BEGIN
			SELECT DISTINCT ee.EmployeeId
				,REPLACE(ee.DomainUsername,'spvn\','') AS NickName
				,ee.NickName AS FullName
				,ee.JoinedDate		
			FROM dbo.Employee AS ee WITH (NOLOCK)
				INNER JOIN #TblEmployeeByTeam AS et ON ee.EmployeeId = et.EmployeeId			
			WHERE 
				ee.IsDeleted = 0
				AND ee.Enabled = 1		
			ORDER BY ee.NickName
		END
		ELSE
		BEGIN
			SELECT DISTINCT ee.EmployeeId
				,REPLACE(ee.DomainUsername,'spvn\','') AS NickName
				,ee.NickName AS FullName
				,ee.JoinedDate		
			FROM dbo.Employee AS ee WITH (NOLOCK)
				INNER JOIN #TblEmployeeByTeam AS et ON ee.EmployeeId = et.EmployeeId
				INNER JOIN #TblEmployeeByProject AS ep ON ee.EmployeeId = ep.EmployeeId
			WHERE 
				ee.IsDeleted = 0
				AND ee.Enabled = 1		
			ORDER BY ee.NickName
		END	
	END
	ELSE IF @IsPM = 1 AND @IsLeader = 1
	BEGIN
		SELECT * 
		FROM
		(
			SELECT DISTINCT ee.EmployeeId
				,REPLACE(ee.DomainUsername,'spvn\','') AS NickName
				,ee.NickName AS FullName
				,ee.JoinedDate		
			FROM dbo.Employee AS ee WITH (NOLOCK)			
				INNER JOIN #TblEmployeeByProject AS ep ON ee.EmployeeId = ep.EmployeeId
			WHERE 
				ee.IsDeleted = 0
				AND ee.Enabled = 1	
			UNION
			SELECT DISTINCT ee.EmployeeId
				,REPLACE(ee.DomainUsername,'spvn\','') AS NickName
				,ee.NickName AS FullName
				,ee.JoinedDate		
			FROM dbo.Employee AS ee WITH (NOLOCK)
				INNER JOIN #TblEmployeeByTeam AS et ON ee.EmployeeId = et.EmployeeId			
			WHERE 
				ee.IsDeleted = 0
				AND ee.Enabled = 1		
		)Sub
		ORDER BY NickName
	END
	ELSE IF @IsPM = 1
	BEGIN
		SELECT DISTINCT ee.EmployeeId
			,REPLACE(ee.DomainUsername,'spvn\','') AS NickName
			,ee.NickName AS FullName
			,ee.JoinedDate		
		FROM dbo.Employee AS ee WITH (NOLOCK)			
			INNER JOIN #TblEmployeeByProject AS ep ON ee.EmployeeId = ep.EmployeeId
		WHERE 
			ee.IsDeleted = 0
			AND ee.Enabled = 1		
		ORDER BY ee.NickName
	END		

	DROP TABLE #TblEmployeeByTeam
	DROP TABLE #TblEmployeeByProject
END