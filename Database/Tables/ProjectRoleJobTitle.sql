USE [NexEPA]
GO

/****** Object:  Table [dbo].[ProjectRoleJobTitle]    Script Date: 8/19/2014 3:40:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectRoleJobTitle](
	[JobTitleId] [int] NOT NULL,
	[ProjectRoleId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectRoleJobTitle] PRIMARY KEY CLUSTERED 
(
	[JobTitleId] ASC,
	[ProjectRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

