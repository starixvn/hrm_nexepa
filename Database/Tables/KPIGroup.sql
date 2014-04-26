USE [NexEPA]
GO

/****** Object:  Table [dbo].[KPIGroup]    Script Date: 4/26/2014 1:24:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KPIGroup](
	[KPIGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DisplayOrder] [int] NULL,
	[IsDisabled] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[TargetTypeId] [tinyint] NULL,
	[LogNo] [varchar(20)] NULL,
 CONSTRAINT [PK_KPIGroupId] PRIMARY KEY CLUSTERED 
(
	[KPIGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[KPIGroup] ADD  CONSTRAINT [DF_KPIGroup_DisplayOrder]  DEFAULT ((100)) FOR [DisplayOrder]
GO

ALTER TABLE [dbo].[KPIGroup] ADD  CONSTRAINT [DF_KPIGroup_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO

