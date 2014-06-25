USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPADetail]    Script Date: 6/19/2014 10:20:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[EPADetail]
CREATE TABLE [dbo].[EPADetail](
	[EPAId] [int] NOT NULL,
	[EvaluatorId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[KPIGroupId] [int] NOT NULL,
	[ScoreId] [int] NULL,
	[IsRequireComment] [bit] NULL,
	[Comment] [nvarchar](max) NULL,
	[IsSpreadKPI] [bit] NULL,
	[KPIWeight] [int] NULL,
	[KPIGroupWeight] [int] NULL,
	[KPILogNo] [varchar](20) NOT NULL,
	[KPIGroupLogNo] [varchar](20) NOT NULL,
	[KPIDisplayOrder] [int] NULL,
	[KPIGroupDisplayOrder] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EPADetailId] PRIMARY KEY CLUSTERED 
(
	[EPAId] ASC,
	[EvaluatorId] ASC,
	[KPIId] ASC,
	[KPIGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

