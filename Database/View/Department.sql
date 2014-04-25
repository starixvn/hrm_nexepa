USE [NexEPA]
GO

/****** Object:  View [dbo].[Department]    Script Date: 4/25/2014 10:39:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Department]
AS
SELECT        Id AS DepartmentId, Code, Name, ManagerId
FROM            HRMDB.NexHRM.dbo.Department AS View_Department WITH (NOLOCK)

GO
