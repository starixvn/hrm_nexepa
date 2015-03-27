USE [NexEPA]
GO
/****** Object:  StoredProcedure [dbo].[ReportRedmine_PersonalEffortDetail]    Script Date: 3/23/2015 1:26:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- select * from employee where NickName = 'Clara.Phan'
-- [dbo].[ReportRedmine_PersonalEffortDetail] '125', '2014-01-01', '2014-12-31',1
ALTER PROCEDURE [dbo].[ReportRedmine_PersonalEffortDetail] 
	@EmployeeId INT
	,@FromDate DATETIME
	,@ToDate DATETIME
	,@Debug BIT = 0
AS
/*
Created: 20150205@Quinn.Vo
Task: Get detail spent time of user.
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON;

	DECLARE @Sql NVARCHAR(MAX)
	DECLARE @NickName NVARCHAR(150)
	DECLARE	@AsgardRedmineName VARCHAR(50)	
	
	SET @AsgardRedmineName ='[REDMINE-ONELAB]'

	SELECT @NickName = REPLACE(DomainUsername,'spvn\','') 
	FROM dbo.Employee WITH (NOLOCK) 
	WHERE EmployeeId = @EmployeeId

	IF OBJECT_ID('#TblSpentTimeDetail') IS NOT NULL
	BEGIN
		DROP TABLE #TblSpentTimeDetail
	END

	CREATE TABLE #TblSpentTimeDetail (
		ProjectId INT
		,ProjectName VARCHAR(255)
		,TrackerId INT
		,TrackerName VARCHAR(30)
		,IssueId INT
		,[Year] INT
		,[Month] INT
		,SpentTime DECIMAL(10, 2)
		)

	IF OBJECT_ID('#TblWorkHours') IS NOT NULL
	BEGIN
		DROP TABLE #TblWorkHours
	END

	CREATE TABLE #TblWorkHours (
		[Year] INT
		,[Month] INT
		,WorkHour INT
		)

	SET @Sql = N'INSERT INTO #TblSpentTimeDetail
				SELECT * FROM OPENQUERY(REDMINE,
				N''SELECT
					ProjectId,
					ProjectName,
					TrackerId,
					TrackerName,
					IssueId,
					Year,
					Month,
					SpentTime
				FROM
				(
					SELECT 
						pr.id AS ProjectId, 
						pr.name AS ProjectName, 
						tc.id AS TrackerId, 
						tc.name AS TrackerName, 
						iss.id AS IssueId, 
						YEAR(te.spent_on) AS Year, 
						MONTH(te.spent_on) As Month, 
						te.hours AS SpentTime,
						te.spent_on AS StartDate,
						u.login AS NickName
					FROM
						projects AS pr
						INNER JOIN time_entries te ON te.project_id = pr.id       
						INNER JOIN users u ON te.user_id = u.id
						INNER JOIN issues AS iss ON te.issue_id = iss.id
						INNER JOIN trackers AS tc ON iss.tracker_id = tc.id
					WHERE pr.id <> 16
					UNION ALL
					SELECT 
						pr.id AS ProjectId, 
						pr.name AS ProjectName, 
						(-1) AS TrackerId, 
						''''Meeting'''' AS TrackerName, 
						m.id AS IssueId, 
						YEAR(m.date) AS Year,
						MONTH(m.date) As Month, 
						(TIME_TO_SEC(SUBTIME(m.end_hour , m.start_hour))/3600) AS SpentTime,
						m.date AS StartDate,
						u.login AS NickName
					FROM projects AS pr
						INNER JOIN meetings AS m ON m.project_id = pr.id
						INNER JOIN meetings_users AS mu ON m.id = mu.meeting_id
						INNER JOIN users AS u ON mu.user_id = u.id
					WHERE pr.id <> 16
				) AS Sub
				WHERE 
					NickName = ''''' + @NickName + ''''' 
					AND StartDate BETWEEN ''''' + CONVERT(VARCHAR(10), @FromDate, 102) + ''''' AND ''''' + CONVERT(VARCHAR(10), @ToDate, 102) + '''''
				;
				'')'

	IF @Debug = 1
	BEGIN
		PRINT @Sql
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

	DECLARE @Month TABLE
	(	[Year] INT, [Month] INT
	)

	INSERT INTO @Month([Year], [Month])
	SELECT DISTINCT YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
	FROM NexHRM.dbo.EmployeeCalendar AS cl WITH (NOLOCK)
		INNER JOIN NexHRM.dbo.Employee AS e WITH (NOLOCK) ON cl.EmpId = e.Id
	WHERE e.Id = @EmployeeId
		AND cl.WorkDate BETWEEN @FromDate AND @ToDate
	ORDER BY YEAR(cl.WorkDate),MONTH(cl.WorkDate)

	-- get work hours from hrm
	INSERT INTO #TblWorkHours (
		[Year]
		,[Month]
		,WorkHour
		)
	SELECT YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
		,SUM(CASE 
				WHEN cl.LeaveDay = 0.5 THEN 0.5
				ELSE 1
				END) * 8
	FROM NexHRM.dbo.EmployeeCalendar AS cl WITH (NOLOCK)
		INNER JOIN NexHRM.dbo.Employee AS e WITH (NOLOCK) ON cl.EmpId = e.Id
	WHERE e.Id = @EmployeeId
		AND cl.WorkDate BETWEEN @FromDate
			AND @ToDate
		AND cl.IsHoliday = 0
		AND cl.IsOffDay = 0
		AND (
			cl.IsLeaveDay = 0
			OR (
				cl.IsLeaveDay = 1
				AND cl.LeaveDay = 0.5
				)
			)
	GROUP BY YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
	ORDER BY YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)

	-- sum issue by project, tracker (cross month)
	SELECT ProjectId
		,ProjectName
		,TrackerId
		,TrackerName
		,COUNT(1) AS NumberIssue
		,SUM(SpentTime) AS SpentTime
	FROM (
		SELECT ProjectId
			,ProjectName
			,TrackerId
			,TrackerName
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY ProjectId
			,ProjectName
			,TrackerId
			,TrackerName
			,IssueId
		) AS TEMP
	GROUP BY ProjectId
		,ProjectName
		,TrackerId
		,TrackerName
	ORDER BY ProjectName
		,TrackerName

	-- detail by project, traker, month
	SELECT ProjectId
		,TrackerId
		,[Year]
		,[Month]
		,COUNT(1) AS NumberIssue
		,SUM(SpentTime) AS SpentTime
	FROM (
		SELECT ProjectId
			,TrackerId
			,[Year]
			,[Month]
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY ProjectId
			,TrackerId
			,IssueId
			,[Year]
			,[Month]
		) AS TEMP
	GROUP BY ProjectId
		,TrackerId
		,[Year]
		,[Month]
	ORDER BY [Year]
		,[Month]

	-- sumary
	SELECT TrackerId
		,TrackerName
		,[Year]
		,[Month]
		,COUNT(1) AS NumberIssue
		,SUM(SpentTime) AS SpentTime
	FROM (
		SELECT TrackerId
			,TrackerName
			,[Year]
			,[Month]
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY TrackerId
			,TrackerName
			,IssueId
			,[Year]
			,[Month]
		) AS TEMP
	GROUP BY TrackerId
		,TrackerName
		,[Year]
		,[Month]
	ORDER BY TrackerName
		,[Year]
		,[Month]

	-- workload by project and month
	SELECT spt.ProjectId
		,wkh.[Year]
		,wkh.[Month]
		,(CASE 
			WHEN ISNULL(wkh.WorkHour, 0) = 0 THEN - 1
			ELSE (spt.SpentTime / wkh.WorkHour) END
			) AS [Workload]
	FROM #TblWorkHours AS wkh
		INNER JOIN (
		SELECT ProjectId
			,[Year]
			,[Month]
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY ProjectId
			,[Year]
			,[Month]
		) AS spt ON wkh.[Year] = spt.[Year]
		AND wkh.[Month] = spt.[Month]

	-- workload by project
	SELECT spt.ProjectId
		,(CASE 
			WHEN ISNULL(wkh.WorkHour, 0) = 0 THEN - 1
			ELSE (spt.SpentTime / wkh.WorkHour) END
			) AS [Workload]
	FROM (
		SELECT ProjectId
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY ProjectId
		) AS spt
		LEFT JOIN 
		(
			SELECT SUM(WorkHour) AS WorkHour FROM #TblWorkHours
		) AS wkh ON 1 = 1

	-- workload by month
	SELECT m.[Year]
		,m.[Month]
		
		,SUBSTRING(DATENAME(MM, CAST((CAST(m.[Year] AS VARCHAR(4)) + '/' 
		+ CAST(m.[Month] AS VARCHAR(2)) + '/01') AS DATETIME)), 1, 3) 
		+ '-' + SUBSTRING(CAST(m.[Year] AS VARCHAR(4)), 3, 2) AS [MonthName]
		,(ISNULL(wkh.WorkHour, 0) / 8.0) AS TotalWorkingDay
		,(CASE 
			WHEN ISNULL(wkh.WorkHour, 0) = 0 THEN 0
			ELSE (ISNULL(spt.SpentTime, 0) / wkh.WorkHour) END
			) AS [Workload]
	FROM 
		@Month AS m
		LEFT JOIN
		#TblWorkHours AS wkh ON m.[Year] = wkh.[Year] AND m.[Month] = wkh.[Month]
		LEFT JOIN (
		SELECT [Year]
			,[Month]
			,SUM(SpentTime) AS SpentTime
		FROM #TblSpentTimeDetail
		GROUP BY [Year]
			,[Month]
		) AS spt ON m.[Year] = spt.[Year]
		AND m.[Month] = spt.[Month]
	
	DROP TABLE #TblSpentTimeDetail
	DROP TABLE #TblWorkHours
END


