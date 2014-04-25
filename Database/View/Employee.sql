USE [NexEPA]
GO

/****** Object:  View [dbo].[Employee]    Script Date: 4/25/2014 10:41:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Employee]
AS
SELECT        Id AS EmployeeId, Code, DomainUsername, FullName, NickName, DOB, Email, JobTitleId, Phone, Gender, GroupId, Roles, JoinedDate, Skype, Yahoo, 
                         PermanentAddress, CurrentAddress, Enabled, ISNULL(isDeleted, 0) AS IsDeleted
FROM            HRMDB.NexHRM.dbo.Employee AS View_Employee WITH (NOLOCK)

GO
