USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetEmployeeKPI]    Script Date: 4/26/2014 4:16:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TargetEmployeeKPI](
	[TargetId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[KPIGroupId] [int] NOT NULL,
	[KPILogNo] [varchar](20) NULL,
	[KPIGroupLogNo] [varchar](20) NULL,
 CONSTRAINT [PK_TargetEmployeeKPI] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC,
	[EmployeeId] ASC,
	[KPIId] ASC,
	[KPIGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

