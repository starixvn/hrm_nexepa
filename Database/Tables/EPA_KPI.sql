USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPA_KPI]    Script Date: 4/17/2014 12:41:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EPA_KPI](
	[EPAId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[ScoreId] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[DisplayOrder] [int] NULL,
	[KPIName] [nvarchar](50) NULL,
	[KPIGroupName] [nvarchar](50) NULL,
	[KPIGroupWeight] [int] NULL
CONSTRAINT [PK_EPAKPI] PRIMARY KEY CLUSTERED
	(
	EPAId,KPIId 
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

