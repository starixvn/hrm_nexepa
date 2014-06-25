USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetProject]    Script Date: 6/19/2014 10:34:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[TargetProject]
CREATE TABLE [dbo].[TargetProject](
	[TargetId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[MilestoneId] [int] NULL,
	[ProjectManager] [int] NOT NULL,
 CONSTRAINT [PK_TargetProject] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

