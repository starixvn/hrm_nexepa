USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetEmployeeProcess]    Script Date: 5/6/2014 3:23:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetEmployeeProcess](
	[TargetId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[EvaluatorId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeKPIProces] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC,
	[EmployeeId] ASC,
	[ProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

