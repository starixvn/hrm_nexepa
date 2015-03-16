USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[ReportRedmine_TeamEffort]    Script Date: 3/16/2015 1:21:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Exec ReportRedmine_TeamEffort '2014/01/01','2014/12/31','Quinn.Vo,Larry.Tran,Josh.Phan,Ricky.Nguyen',1
ALTER PROCEDURE [dbo].[ReportRedmine_TeamEffort]	
	@FromDate DATETIME
	,@ToDate DATETIME	
	,@NickName VARCHAR(3000)
	,@ProjectId INT	= NULL
	,@Debug BIT = 0
AS
/*
Created: 20150225@Larry.Tran
Task: Get project effort of every member in a team.
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

	IF OBJECT_ID('#TblTeamEffort') IS NOT NULL
	BEGIN
		DROP TABLE #TblTeamEffort
	END

	CREATE TABLE #TblTeamEffort (
		NickName VARCHAR(150)
		,FullName VARCHAR(150)		
		,ProjectId INT
		,ProjectName VARCHAR(255)		
		,[Year] INT
		,[Month] INT
		,SpentTime DECIMAL(10, 2)
	)

	IF OBJECT_ID('#TblTeamWorkHours') IS NOT NULL
	BEGIN
		DROP TABLE #TblTeamWorkHours
	END

	CREATE TABLE #TblTeamWorkHours (
		NickName VARCHAR(150)		
		,[Year] INT
		,[Month] INT
		,WorkHour INT
	)
	
	IF OBJECT_ID('#TblTeamWorkingMonths') IS NOT NULL
	BEGIN
		DROP TABLE #TblTeamWorkingMonths
	END

	CREATE TABLE #TblTeamWorkingMonths (
		[Year] INT
		,[Month] INT
		,[MonthName] VARCHAR(10)
	)	

	SET @NickName = CONCAT('''''', REPLACE(@NickName,',',''''','''''),'''''')

	SET @Sql = N'INSERT INTO #TblTeamEffort
				SELECT * FROM OPENQUERY(REDMINE,
				N''SELECT
						u.login,						
						u.firstname,
						pr.id AS ProjectId,
						pr.name AS ProjectName,
						YEAR(te.spent_on) AS Year,
						MONTH(te.spent_on) AS Month,
						SUM(te.hours) AS SpentTime
					FROM
						projects AS pr
						INNER JOIN time_entries AS te ON te.project_id = pr.id
						INNER JOIN users AS u ON te.user_id = u.id
					WHERE
						pr.id <> 16
						AND u.login IN ('+@NickName+')
						AND te.spent_on BETWEEN ''''' + CONVERT(VARCHAR(10), @FromDate, 102) + ''''' AND ''''' + CONVERT(VARCHAR(10), @ToDate, 102) + '''''
					GROUP BY u.login,u.firstname, ProjectId , ProjectName , Year , Month
					ORDER BY Year , Month;
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

	IF @Debug = 1
	BEGIN
		SELECT @Sql
	END

	EXEC(@Sql)

	--Filter by Project if any
	IF @ProjectId IS NOT NULL
	BEGIN
		DELETE #TblTeamEffort WHERE ProjectId <> @ProjectId
	END

	-- get work hours from hrm
	INSERT INTO #TblTeamWorkHours
	SELECT
		REPLACE(e.DomainUsername,'spvn\','') AS NickName					
		,YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
		,SUM(CASE 
				WHEN cl.LeaveDay = 0.5 THEN 0.5
				ELSE 1
				END) * 8
	FROM NexHRM.dbo.EmployeeCalendar AS cl WITH (NOLOCK)
		INNER JOIN NexHRM.dbo.Employee AS e WITH (NOLOCK) ON cl.EmpId = e.Id
	WHERE e.NickName IN (SELECT DISTINCT FullName FROM #TblTeamEffort)
		AND cl.WorkDate BETWEEN @FromDate AND @ToDate
		AND cl.IsHoliday = 0
		AND cl.IsOffDay = 0
		AND (
			cl.IsLeaveDay = 0
			OR (
				cl.IsLeaveDay = 1
				AND cl.LeaveDay = 0.5
				)
			)
	GROUP BY REPLACE(e.DomainUsername,'spvn\','')					
		,YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
	ORDER BY NickName					
		,YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)	

	-- get working month from hrm
	INSERT INTO #TblTeamWorkingMonths
	SELECT DISTINCT YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
		,REPLACE(RIGHT(CONVERT(VARCHAR(9), cl.WorkDate, 6), 6), ' ', '-') AS [MonthName]
	FROM NexHRM.dbo.EmployeeCalendar AS cl WITH (NOLOCK)
		INNER JOIN NexHRM.dbo.Employee AS e WITH (NOLOCK) ON cl.EmpId = e.Id
	WHERE e.NickName IN (SELECT DISTINCT FullName FROM #TblTeamEffort)
		AND cl.WorkDate BETWEEN @FromDate AND @ToDate
	ORDER BY YEAR(cl.WorkDate),MONTH(cl.WorkDate)	

	--Team Effort Detail
	SELECT NickName,FullName,ProjectId,ProjectName,[Year],[Month],SpentTime
	FROM #TblTeamEffort
	
	--Member Project
	SELECT NickName,FullName,ProjectId,
		ProjectName, SUM(SpentTime) AS SubTotal
	FROM #TblTeamEffort		
	GROUP BY NickName,FullName,ProjectId,ProjectName
	ORDER BY NickName,ProjectName

	--Month
	SELECT * 
	FROM #TblTeamWorkingMonths
	ORDER BY [Year],[Month]

	--Member & Sub Total
	SELECT NickName, [Year],[Month],SUM(SpentTime) AS SpentTime
	FROM #TblTeamEffort		
	GROUP BY NickName, [Year],[Month]

	--Member Working days
	SELECT NickName,[Year],[Month],WorkHour/8.0 AS WorkingDay
	FROM #TblTeamWorkHours
	ORDER BY NickName,[Year],[Month]

	--Member Workload
	SELECT 
		h.NickName,h.[Year],h.[Month],
		(CASE WHEN ISNULL(h.WorkHour,0) = 0 
			THEN 0
			ELSE ISNULL(Sub.SpentTime,0)/h.WorkHour END) AS [Workload]
	FROM 
		#TblTeamWorkHours AS h
		INNER JOIN
		(
			SELECT NickName, [Year],[Month],SUM(SpentTime) AS SpentTime
			FROM #TblTeamEffort
			GROUP BY NickName, [Year],[Month]
		) AS Sub
		ON h.NickName = Sub.NickName AND h.[Year] = Sub.[Year] AND h.[Month] = Sub.[Month]
	
	--Total Efforts
	SELECT [YEAR],[MONTH],SUM(SpentTime) Total
	FROM #TblTeamEffort
	GROUP BY [YEAR],[MONTH]
	
	DROP TABLE #TblTeamEffort
	DROP TABLE #TblTeamWorkHours
	DROP TABLE #TblTeamWorkingMonths
END