USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPAProcess]    Script Date: 6/19/2014 10:17:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[EPAProcess]
CREATE TABLE [dbo].[EPAProcess](
	[EPAId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[EvaluatorId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsEndProcess] [bit] NOT NULL,
	[KPICount] [int] NULL,
	[AverageScore] [decimal](18, 2) NULL,
	[NoticeAtBegin] [bit] NULL,
	[RemindDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_EPAProcess] PRIMARY KEY CLUSTERED 
(
	[EPAId] ASC,
	[ProcessId] ASC,
	[EvaluatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

