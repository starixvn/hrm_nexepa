USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPA]    Script Date: 4/17/2014 1:16:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EPA](
	[EPAId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[TargetId] [int] NULL,
	[EvaluatorId] [int] NULL,
	[Score] [decimal](18, 2) NULL,
	[Comment] [nvarchar](max) NULL,
	[MarkedBy] [int] NULL,
	[MarkedDate] [datetime] NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_EPA] PRIMARY KEY CLUSTERED 
(
	[EPAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

