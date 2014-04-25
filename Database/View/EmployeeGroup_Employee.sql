USE [NexEPA]
GO

/****** Object:  View [dbo].[EmployeeGroup_Employee]    Script Date: 4/25/2014 10:43:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EmployeeGroup_Employee]
AS
SELECT        GroupId, EmpID, isMainGroup, ISNULL(HandleRequest, 0) AS HandleRequest
FROM            HRMDB.NexHRM.dbo.EmployeeGroup_Employee AS View_EmployeeGroup_Employee WITH (NOLOCK)

GO


