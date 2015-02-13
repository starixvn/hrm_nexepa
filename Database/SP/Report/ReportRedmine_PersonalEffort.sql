/****** Object:  StoredProcedure [dbo].[ReportRedmine_PersonalEffort]    Script Date: 2/13/2015 9:09:23 AM ******/
IF OBJECT_ID('[dbo].[ReportRedmine_PersonalEffort]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[ReportRedmine_PersonalEffort]
END
GO
/****** Object:  StoredProcedure [dbo].[ReportRedmine_PersonalEffort]    Script Date: 2/13/2015 9:29:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[ReportRedmine_PersonalEffort]
	@EmployeeId INT,
	@FromDate DATETIME,
	@ToDate DATETIME,
	@Debug BIT = 0
AS
/*
Created: 20150206@Larry.Tran
Task: Get the project efforts from Redmine database
DB: NexEPA
Original: 

Revisions:  
Param's Explanation:

*/
BEGIN
	SET NOCOUNT ON

	DECLARE @Sql NVARCHAR(MAX)
	DECLARE @PivotColumns NVARCHAR(3000)
	DECLARE @NickName NVARCHAR(150)
	
	SELECT @NickName = NickName FROM dbo.Employee WITH (NOLOCK) WHERE EmployeeId = @EmployeeId

	IF OBJECT_ID('#TblProjectEffort') IS NOT NULL
	BEGIN
		DROP TABLE #TblProjectEffort
	END
	
	CREATE TABLE #TblProjectEffort(				
		ProjectName VARCHAR(255),
		[Year] INT,
		[YearMonth] INT,
		[MonthName] VARCHAR(10),
		SpentTime DECIMAL(10,2)
	)
	
	IF OBJECT_ID('#TblProjectEffort_Month') IS NOT NULL
		DROP TABLE #TblProjectEffort_Month
	
	CREATE TABLE #TblProjectEffort_Month(
		[Year] INT,
		[YearMonth] INT,
		[MonthName] VARCHAR(10),
		TotalSpentTimeOfMonth DECIMAL(10,2)
	)
	
	IF OBJECT_ID('#TblWorkingMonths') IS NOT NULL
	BEGIN
		DROP TABLE #TblWorkingMonths
	END

	CREATE TABLE #TblWorkingMonths (
		[Year] INT
		,[Month] INT
		,[MonthName] VARCHAR(10)
	)	

	SET @Sql = N'INSERT INTO #TblProjectEffort
				SELECT * FROM OPENQUERY(REDMINE,
				N''SELECT p.name ProjectName,Year(te.spent_on) Year, Month(te.spent_on) YearMonth,
						DATE_FORMAT(te.spent_on,''''%b-%y'''') MonthName,SUM(te.hours) SpentTime 
				FROM redmine.projects p
					INNER JOIN redmine.time_entries te ON te.project_id = p.id       
					INNER JOIN redmine.users u ON te.user_id = u.id
				WHERE 
				p.id <> 16 AND u.login LIKE N'''''+ ISNULL(@NickName,' ') + '''''
				AND te.spent_on BETWEEN '''''+CONVERT(VARCHAR(10),@FromDate,102)+''''' AND '''''+CONVERT(VARCHAR(10),@ToDate,102)+'''''
				GROUP BY p.name,MonthName
				ORDER BY YEAR(te.spent_on),MONTH(te.spent_on),p.name;
				'')'
	
	IF @Debug = 1
	BEGIN
		SELECT @Sql
	END
	
	EXEC(@Sql)

	INSERT INTO #TblProjectEffort_Month
	SELECT DISTINCT p.[Year],p.YearMonth,p.[MonthName],
		(SELECT SUM(SpentTime) 
		FROM #TblProjectEffort sub 
		WHERE sub.[MonthName] = p.[MonthName]) TotalSpentTimeOfMonth
	FROM #TblProjectEffort p

	-- get working month from hrm
	INSERT INTO #TblWorkingMonths
	SELECT DISTINCT YEAR(cl.WorkDate)
		,MONTH(cl.WorkDate)
		,REPLACE(RIGHT(CONVERT(VARCHAR(9), cl.WorkDate, 6), 6), ' ', '-') AS [MonthName]
	FROM NexHRM.dbo.EmployeeCalendar AS cl WITH (NOLOCK)
		INNER JOIN NexHRM.dbo.Employee AS e WITH (NOLOCK) ON cl.EmpId = e.Id
	WHERE e.NickName = @NickName
		AND cl.WorkDate BETWEEN @FromDate AND @ToDate
	ORDER BY YEAR(cl.WorkDate),MONTH(cl.WorkDate)

	-- Project Efforts data
	SELECT ProjectName,[MonthName],SpentTime FROM #TblProjectEffort

	-- Project info & total spent time of all months by project
	SELECT DISTINCT p.ProjectName,
		(SELECT SUM(SpentTime) 
		FROM #TblProjectEffort sub 
		WHERE sub.ProjectName = p.ProjectName) TotalSpentTimeOfAllMonths
	FROM #TblProjectEffort p
	ORDER BY p.ProjectName
	
	-- Month info & total spent time of all projects by month
	SELECT w.[MonthName],p.TotalSpentTimeOfMonth 
	FROM #TblWorkingMonths w LEFT JOIN #TblProjectEffort_Month p
		ON w.[Year] = p.[Year] AND w.[Month] = p.YearMonth
	ORDER BY w.[Year],w.[Month]

	DROP TABLE #TblProjectEffort	
	DROP TABLE #TblProjectEffort_Month
	DROP TABLE #TblWorkingMonths
END

GO


