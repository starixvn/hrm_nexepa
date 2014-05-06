USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetSettingPeriod]    Script Date: 5/6/2014 3:23:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetSettingPeriod](
	[TargetType] [tinyint] NOT NULL,
	[DepartmentId] [int] NULL,
	[EmployeeLevelId] [int] NULL,
	[TargetTemplateId] [int] NOT NULL,
	[RefNo] [int] IDENTITY(1,1) NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO

