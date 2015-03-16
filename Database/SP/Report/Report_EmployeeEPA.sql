USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[Report_EmployeeEPA]    Script Date: 3/16/2015 1:18:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Report_EmployeeEPA] 
	@ActorUserId INT
	,@EmployeeId INT
	,@FromDate DATETIME
	,@ToDate DATETIME
AS
/*
Created: 20140508@Quinn.Vo
Task: Get data for employee EPA report.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @IsSuppervisor BIT
	SET @IsSuppervisor = 0

	SELECT @IsSuppervisor = 1
	FROM dbo.EmployeeRole WITH (NOLOCK)
	WHERE EmployeeId = @ActorUserId
		AND RoleId = 1 -- System Admin

	SELECT e.EmployeeId
		,e.Code
		,e.DomainUsername
		,e.NickName
		,e.FullName
		,e.Email
		,STUFF((
				SELECT CAST(', ' AS VARCHAR(MAX)) + sr.NAME
				FROM dbo.EmployeeRole AS er WITH (NOLOCK)
					INNER JOIN dbo.SystemRole AS sr WITH (NOLOCK) ON er.RoleId = sr.RoleId
				WHERE er.EmployeeId = e.EmployeeId
				FOR XML PATH('')
				), 1, 1, '') AS Roles
		,g.GroupId
		,g.Code
		,g.NAME
		,job.JobTitleId
		,job.Code
		,job.NAME
	FROM dbo.Employee AS e WITH (NOLOCK)
		INNER JOIN dbo.EmployeeGroup AS g WITH (NOLOCK) ON e.GroupId = g.GroupId
		INNER JOIN dbo.JobTitle AS job WITH (NOLOCK) ON e.JobTitleId = job.JobTitleId
	WHERE e.EmployeeId = @EmployeeId

	SELECT tg.TargetId
		,tg.Name
		,tg.StartDate
		,tg.EndDate
		,STUFF((
				SELECT CAST(', ' AS VARCHAR(MAX)) + rl.NAME
				FROM dbo.ProjectMemberRole AS pmr WITH (NOLOCK)
					INNER JOIN dbo.ProjectRole AS rl WITH (NOLOCK) ON pmr.ProjectRoleId = rl.ProjectRoleId
				WHERE pmr.ProjectId = mb.ProjectId
					AND pmr.EmployeeId = mb.EmployeeId
				FOR XML PATH('')
				), 1, 1, '') AS Roles
		,epa.FinalScore AS Score
		,pc.Comment
		,pr.ProjectId
		,pr.NAME
		,pr.StartDate
		,pr.EndDate
		,pm.EmployeeId
		,pm.NickName
		,pm.FullName
		,pml.MilestoneId
		,pml.NAME
		,pml.StartDate
		,pml.EndDate
	FROM 
		dbo.[Target] AS tg WITH (NOLOCK)
		INNER JOIN dbo.TargetProject AS tp WITH (NOLOCK) ON tg.TargetId = tp.TargetId
		INNER JOIN dbo.EPA AS epa WITH (NOLOCK) ON tg.TargetId = epa.TargetId
		INNER JOIN dbo.EPAProcess AS pc WITH (NOLOCK) ON epa.EPAId = pc.EPAId
		INNER JOIN dbo.Project AS pr WITH (NOLOCK) ON tp.ProjectId = pr.ProjectId
		INNER JOIN dbo.Employee AS pm WITH (NOLOCK) ON pr.ProjectManager = pm.EmployeeId
		INNER JOIN dbo.Employee AS ee WITH (NOLOCK) ON epa.EmployeeId = ee.EmployeeId
		INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON ee.GroupId = eg.GroupId
		LEFT JOIN dbo.ProjectMilestone AS pml ON tp.MilestoneId = pml.MilestoneId
		LEFT JOIN dbo.ProjectMember AS mb WITH (NOLOCK) 
			ON mb.EmployeeId = @EmployeeId
			AND pr.ProjectId = mb.ProjectId		
	WHERE  epa.EmployeeId = @EmployeeId AND ee.IsDeleted = 0 AND ee.Enabled = 1
		AND (@IsSuppervisor = 1 OR @EmployeeId = @ActorUserId 
			OR pc.EvaluatorId = @ActorUserId
			OR pr.ProjectManager = @ActorUserId
			OR (epa.EmployeeId IN (SELECT DISTINCT EmployeeId 
									FROM dbo.fnc_GetAllMembersOfLeader(@ActorUserId)))
		)
		AND (tg.StartDate <= @ToDate AND tg.EndDate >= @FromDate)	
END
