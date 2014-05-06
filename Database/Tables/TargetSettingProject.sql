USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetSettingProject]    Script Date: 5/6/2014 3:23:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetSettingProject](
	[TargetTypeId] [tinyint] NOT NULL,
	[ProjectRoleId] [int] NOT NULL,
	[TargetTemplateId] [int] NOT NULL,
 CONSTRAINT [PK_TargetSettingProject] PRIMARY KEY CLUSTERED 
(
	[TargetTypeId] ASC,
	[ProjectRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

