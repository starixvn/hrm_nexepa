USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[GroupProject_Get]    Script Date: 04/15/2015 17:16:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GroupProject_Get]
AS
/*
Created: 20150414@Tim.Trinh
Task: Manage Group Project.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON
	
	IF OBJECT_ID('#TblProject') IS NOT NULL
	BEGIN
		DROP TABLE #TblProject
	END
	
	CREATE TABLE #TblProject(
		ProjectId INT
		,ProjectName NVARCHAR(255)
		,DatabaseId INT
	)
	
	INSERT INTO #TblProject	
	SELECT Id AS ProjectId
		,Name AS ProjectName
		,1 AS DatabaseId
	FROM OPENQUERY([REDMINE],
		'SELECT id, name 
		FROM projects 
		WHERE id <> 16')
	UNION
	SELECT Id AS ProjectId
		,Name AS ProjectName
		,2 AS DatabaseId
		FROM OPENQUERY([REDMINE-ONELAB],
		'SELECT id, name 
		FROM projects
		WHERE id <> 1')
	
	SELECT [GroupProjectId]
		,[Name]
	FROM [dbo].[GroupProject] WITH(NOLOCK)
	
	SELECT DISTINCT gp.[GroupProjectId]
		,gp.[Name]
		,gpd.[ProjectId]
		,gpd.[DatabaseId]
		,p.[ProjectName]
	FROM [dbo].[GroupProject] gp WITH(NOLOCK) 
	INNER JOIN [dbo].[GroupProjectDetail] gpd WITH(NOLOCK)
		ON gp.[GroupProjectId] = gpd.[GroupProjectId]
     INNER JOIN #TblProject p WITH(NOLOCK)
		ON gpd.[ProjectId] = p.[ProjectId]
			AND gpd.[DatabaseId] = p.[DatabaseId]
    	   
    IF OBJECT_ID('#TblProject') IS NOT NULL
	BEGIN
		DROP TABLE #TblProject
	END
END