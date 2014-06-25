USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetType]    Script Date: 6/19/2014 3:12:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetType](
	[TargetTypeId] [tinyint] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[IsDisabled] [bit] NULL,
	[DisplayOrder] [tinyint] NULL,
	[ScoreTypeId] [tinyint] NULL,
	[ProcessGroupId] [int] NULL,
	[TargetTemplateId] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TargetTypeId] PRIMARY KEY CLUSTERED 
(
	[TargetTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

