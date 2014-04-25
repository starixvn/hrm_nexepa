USE [NexEPA]
GO

/****** Object:  View [dbo].[JobTitle]    Script Date: 4/25/2014 10:43:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[JobTitle]
AS
SELECT        Id AS JobTitleId, Code, Name
FROM            HRMDB.NexHRM.dbo.LookupData AS View_JobTitle WITH (NOLOCK)
WHERE        (LookupId = 1)

GO


