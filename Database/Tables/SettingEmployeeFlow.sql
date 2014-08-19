USE [NexEPA]
GO

/****** Object:  Table [dbo].[SettingEmployeeFlow]    Script Date: 8/19/2014 3:41:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SettingEmployeeFlow](
	[EmployeeId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[EvaluatorId] [int] NOT NULL,
	[Priority] [tinyint] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeFlowSetting1] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[ProcessId] ASC,
	[EvaluatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

