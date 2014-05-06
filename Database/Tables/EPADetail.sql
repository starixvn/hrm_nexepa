USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPA_KPI]    Script Date: 4/17/2014 12:41:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[EPADetail]
CREATE TABLE [dbo].[EPADetail] (
	[EPAId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[KPIGroupId] [int] NOT NULL,
	[ScoreId] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[IsRequireComment] [bit] NULL,
	[KPIWeight] [int] NULL,
	[KPIGroupWeight] [int] NULL,
	[KPILogNo] [varchar](20) NULL,
	[KPIGroupLogNo] [varchar](20) NULL,
	[DisplayOrder] [int] NULL
CONSTRAINT [PK_EPAKPIKPIGroupId] PRIMARY KEY CLUSTERED
	(
	EPAId,KPIId,KPIGroupId
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

