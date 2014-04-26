USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetTemplateKPIGroup]    Script Date: 4/26/2014 9:39:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[TargetTemplateKPIGroup]

CREATE TABLE [dbo].[TargetTemplateKPIGroup](
	[TargetTemplateId] [int] NOT NULL,
	[KPIGroupId] [int] NOT NULL,
	[Weight] [int] NOT NULL,
	[DisplayOrder] [int] NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TargetTemplateKPIGroup] PRIMARY KEY CLUSTERED 
(
	[TargetTemplateId] ASC,
	[KPIGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TargetTemplateKPIGroup] ADD  CONSTRAINT [DF_TargetTemplateKPIGroup_Weight]  DEFAULT ((0)) FOR [Weight]
GO

ALTER TABLE [dbo].[TargetTemplateKPIGroup] ADD  CONSTRAINT [DF_TargetTemplateKPIGroup_DisplayOrder]  DEFAULT ((100)) FOR [DisplayOrder]
GO

