USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetTemplate]    Script Date: 3/5/2014 2:55:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[TargetTemplate]

CREATE TABLE [dbo].[TargetTemplate](
	[TargetTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TargetTypeId] [tinyint] NOT NULL,
	[IsDisabled] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
	
CONSTRAINT [PK_TargetTemplateId] PRIMARY KEY CLUSTERED 
(
	[TargetTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

