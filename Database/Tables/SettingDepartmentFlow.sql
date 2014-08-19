USE [NexEPA]
GO

/****** Object:  Table [dbo].[SettingDepartmentFlow]    Script Date: 8/19/2014 3:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SettingDepartmentFlow](
	[DepartmentId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[EvaluatorId] [int] NOT NULL,
	[Priority] [tinyint] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeFlowSetting] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[ProcessId] ASC,
	[EvaluatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

