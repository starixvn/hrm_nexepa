USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetSetting]    Script Date: 3/5/2014 2:47:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetSetting](
	[TargetType] [tinyint] NOT NULL,
	[DepartmentId] [int] NULL,
	[EmployeeLevelId] [int] NULL,
	[TargetId] [int] NOT NULL,
	[RefNo] [int] IDENTITY(1,1) NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO

