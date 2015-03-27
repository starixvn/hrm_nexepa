USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[ProjectRedmine_GetAllProjects]    Script Date: 3/23/2015 1:27:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC ProjectRedmine_GetAllProjects 1
ALTER PROCEDURE [dbo].[ProjectRedmine_GetAllProjects]	
	@Debug BIT = 0
AS
/*
Created: 20150311@Larry.Tran
Task: Get all redmine projects.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @Sql NVARCHAR(MAX)
	DECLARE	@AsgardRedmineName VARCHAR(50)	
	
	SET @AsgardRedmineName ='[REDMINE-ONELAB]'	

	IF OBJECT_ID('#TblProjects') IS NOT NULL
	BEGIN
		DROP TABLE #TblProjects
	END

	CREATE TABLE #TblProjects (			
		ProjectId INT
		,ProjectName VARCHAR(255)		
	)	

	SET @Sql = N'INSERT INTO #TblProjects
				SELECT * FROM OPENQUERY(REDMINE,
				N''SELECT DISTINCT						
						pr.id AS ProjectId,
						pr.name AS ProjectName						
					FROM
						projects AS pr
						INNER JOIN time_entries AS te ON te.project_id = pr.id
						INNER JOIN users AS u ON te.user_id = u.id
					WHERE
						pr.id <> 16					
					ORDER BY pr.name;
				'')'

	IF @Debug = 1
	BEGIN
		SELECT @Sql
	END

	EXEC (@Sql)

	--Get data from Asgard Redmine
	SET @Sql = REPLACE(@Sql,'(REDMINE,','('+@AsgardRedmineName+',')

	--Make sure projectId is unique, we change projectId to negative number
	SET @Sql = REPLACE(@Sql,'pr.id AS ProjectId,','pr.id*-1 AS ProjectId,')

	--Asgard: ProjectId = 1 (Demo project) => exclude this project
	SET @Sql = REPLACE(@Sql,'pr.id <> 16','pr.id <> 1')

	IF @Debug = 1
	BEGIN
		SELECT @Sql
	END

	EXEC(@Sql)

	SELECT ProjectId,LTRIM(RTRIM(ProjectName)) AS ProjectName
	FROM #TblProjects
	ORDER BY LTRIM(RTRIM(ProjectName))
	
	DROP TABLE #TblProjects	
END