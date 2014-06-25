USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetPeriod]    Script Date: 6/19/2014 9:58:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetPeriod](
	[TargetId] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EPATypeId] [int] NOT NULL,
	[IsShow] [bit] NOT NULL,
 CONSTRAINT [PK_TargetPeriod] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TargetPeriod] ADD  CONSTRAINT [DF_TargetPeriod_IsShow]  DEFAULT ((1)) FOR [IsShow]
GO

