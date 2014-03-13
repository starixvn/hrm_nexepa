USE [NexEPA]
GO

/****** Object:  Table [dbo].[KPITargetTemplate]    Script Date: 3/5/2014 2:05:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetKPITemplate](
	[TargetId] [int] NOT NULL,
	[KPIId] [int] NOT NULL,
	[ProjectRoleId] [int] NULL,
	[DisplayOrder] [int] NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL
CONSTRAINT [PK_TargetKPITemplate] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC,
	[KPIId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
