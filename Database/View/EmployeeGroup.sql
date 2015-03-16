USE [NexEPA]
GO

/****** Object:  View [dbo].[EmployeeGroup]    Script Date: 4/25/2014 10:42:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EmployeeGroup]
AS
SELECT        Id AS GroupId, Code, Name, DepartmentId, LeaderId, AssistantLeaderId, Rank, ISNULL(Deleted, 0) AS IsDeleted, ParentId
FROM            HRMDB.NexHRM.dbo.EmployeeGroup AS View_EmployeeGroup WITH (NOLOCK)

GO


