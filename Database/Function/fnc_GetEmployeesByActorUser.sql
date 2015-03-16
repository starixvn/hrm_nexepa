USE [NexEPA]
GO
/****** Object:  UserDefinedFunction [dbo].[fnc_GetEmployeesByActorUser]    Script Date: 3/16/2015 1:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fnc_GetEmployeesByActorUser] 
(	
	@ActorUserId INT
)
RETURNS @TblEmployees TABLE(EmployeeId INT,GroupId INT)
/*
Created: 20150311@Larry.Tran
Task: Get Employees by Actor User.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/ 
AS
BEGIN
	DECLARE @IsSystemAdmin BIT
	DECLARE @IsPM BIT
	DECLARE @IsLeader BIT

	SELECT @IsSystemAdmin = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 1
	SELECT @IsPM = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 3
	SELECT @IsLeader = 1 FROM dbo.EmployeeRole WITH (NOLOCK) WHERE EmployeeId = @ActorUserId AND RoleId = 4

	IF (@IsSystemAdmin = 1)
	BEGIN
		INSERT @TblEmployees
		SELECT e.[EmployeeId],e.GroupId			
		FROM dbo.Employee AS e WITH (NOLOCK)
			INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON e.GroupId = eg.GroupId
		WHERE eg.IsDeleted = 0 AND e.Enabled = 1 AND e.IsDeleted = 0		
	END
	ELSE
	BEGIN
		IF (@IsPM = 1)
		BEGIN
			INSERT @TblEmployees
			--Actor is PM
			SELECT DISTINCT e.[EmployeeId],e.GroupId
			FROM dbo.Employee AS e WITH (NOLOCK)
				INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON e.GroupId = eg.GroupId
				INNER JOIN ProjectMember AS m WITH (NOLOCK) ON e.EmployeeId = m.EmployeeId AND m.IsDisabled = 0
				INNER JOIN Project AS p WITH (NOLOCK) ON p.ProjectId = m.ProjectId
			WHERE p.ProjectManager = @ActorUserId
				AND eg.IsDeleted = 0 AND e.Enabled = 1 AND e.IsDeleted = 0
			UNION
			--Actor is Evaluator
			SELECT DISTINCT e.[EmployeeId],e.GroupId
			FROM dbo.Employee AS e WITH (NOLOCK)
			INNER JOIN dbo.EmployeeGroup AS eg WITH (NOLOCK) ON e.GroupId = eg.GroupId
			INNER JOIN EPA AS epa WITH (NOLOCK) ON epa.EmployeeId = e.EmployeeId
			INNER JOIN EPAProcess AS ep WITH (NOLOCK) ON epa.EPAId = ep.EPAId
			INNER JOIN Target AS tg WITH (NOLOCK) ON tg.TargetId = epa.TargetId
			INNER JOIN TargetProject AS tp WITH (NOLOCK) ON tp.TargetId = tg.TargetId
			WHERE ep.EvaluatorId = @ActorUserId
				AND eg.IsDeleted = 0 AND e.Enabled = 1 AND e.IsDeleted = 0				
		END

		IF (@IsLeader = 1)
		BEGIN			
			INSERT @TblEmployees
			SELECT DISTINCT e.EmployeeId,e.GroupId
			FROM dbo.fnc_GetAllMembersOfLeader(@ActorUserId) AS e
				LEFT JOIN @TblEmployees AS mb ON e.EmployeeId = mb.EmployeeId
			WHERE mb.EmployeeId IS NULL
		END
	END
	
	RETURN
END
