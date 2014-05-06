USE [NexEPA]
GO

/****** Object:  Table [dbo].[Process]    Script Date: 5/6/2014 3:19:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Process](
	[ProcessId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ProcessGroupId] [int] NOT NULL,
	[ProcessDays] [int] NULL,
	[Priority] [tinyint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsEvaluatedByPM] [bit] NULL,
	[IsEveluatedByLeader] [bit] NULL,
	[UpperLevel] [tinyint] NULL,
	[IsSelfEvaluated] [bit] NULL,
 CONSTRAINT [PK_ProcessId] PRIMARY KEY CLUSTERED 
(
	[ProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

