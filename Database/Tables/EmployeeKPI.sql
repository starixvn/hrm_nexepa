USE [NexEPA]
GO

/****** Object:  Table [dbo].[EmployeeKPI]    Script Date: 3/5/2014 10:51:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeKPI](
	[EmployeeId] [int] NOT NULL,
	[TargetId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[ScoreId] [int] NULL,
	[MarkedBy] [int] NULL,
	[MarkedDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[DisplayOrder] [int] NULL,
	[KPIName] [nvarchar](50) NULL,
	[KPIGroupName] [nvarchar](50) NULL
CONSTRAINT [PK_EmployeeKPI] PRIMARY KEY CLUSTERED 
	(
	EmployeeId,TargetId,KPIId,ProcessId 
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GO

