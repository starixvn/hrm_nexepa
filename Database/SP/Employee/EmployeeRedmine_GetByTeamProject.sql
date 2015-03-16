USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeRedmine_GetByTeamProject]    Script Date: 3/16/2015 1:21:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC EmployeeRedmine_GetByTeamProject NULL, 132, 1
ALTER PROCEDURE [dbo].[EmployeeRedmine_GetByTeamProject]	
	@GroupId INT = NULL
	,@ProjectId INT = NULL
	,@Debug BIT = 0
AS
/*
Created: 20150312@Larry.Tran
Task: Get employees by team and redmine projects.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @Sql NVARCHAR(MAX)
	DECLARE @LinkedServerName VARCHAR(50)
	DECLARE	@AsgardRedmineName VARCHAR(50)	
	
	SET @LinkedServerName = 'REDMINE'
	SET @AsgardRedmineName ='[REDMINE-ONELAB]'	

	IF OBJECT_ID('#TblRedmineEmployees') IS NOT NULL
	BEGIN
		DROP TABLE #TblRedmineEmployees
	END

	CREATE TABLE #TblRedmineEmployees (			
		NickName VARCHAR(150)		
	)	

	IF @ProjectId IS NOT NULL
	BEGIN
		--Filter by Asgard Redmine
		IF @ProjectId < 0
		BEGIN
			SET @ProjectId = @ProjectId * -1
			SET @LinkedServerName = @AsgardRedmineName
		END
		
		SET @Sql = N'INSERT INTO #TblRedmineEmployees
					SELECT * FROM OPENQUERY('+@LinkedServerName+',
					N''SELECT DISTINCT
							u.login AS NickName						
						FROM
							projects AS pr
							INNER JOIN time_entries AS te ON te.project_id = pr.id
							INNER JOIN users AS u ON te.user_id = u.id
						WHERE
							pr.id <> 16
							AND pr.id = '+CONVERT(VARCHAR(15),@ProjectId)+'					
						ORDER BY pr.name;
					'')'

		IF @Debug = 1
		BEGIN
			SELECT @Sql
		END

		EXEC (@Sql)		
	END

	SELECT DISTINCT e.EmployeeId
		,REPLACE(e.DomainUsername,'spvn\','') AS NickName
		,e.NickName AS FullName
		,e.JoinedDate		
	FROM dbo.Employee AS e WITH (NOLOCK)			
		INNER JOIN EmployeeGroup AS eg WITH (NOLOCK) ON e.GroupId = eg.GroupId
	WHERE 
		e.IsDeleted = 0
		AND e.Enabled = 1
		AND eg.IsDeleted = 0
		AND (@GroupId IS NULL OR eg.GroupId = @GroupId)
		AND (@ProjectId IS NULL 
			OR REPLACE(e.DomainUsername,'spvn\','') IN (SELECT NickName FROM #TblRedmineEmployees)
		)
	ORDER BY e.NickName
	
	DROP TABLE #TblRedmineEmployees	
END