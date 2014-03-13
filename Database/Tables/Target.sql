USE [NexEPA]
GO

/****** Object:  Table [dbo].[Target]    Script Date: 3/5/2014 2:38:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Target](
	[TargetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TargetTypeId] [tinyint] NOT NULL,
	[ScoreTypeId] [tinyint] NOT NULL,
	[ProcessGroupId] [int] NOT NULL,
	[ProjectId] [int] NULL,
	[MilestoneId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [tinyint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
CONSTRAINT [PK_TargetId] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

