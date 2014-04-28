USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetEmployee]    Script Date: 3/5/2014 2:40:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[TargetEmployee]
CREATE TABLE [dbo].[TargetEmployee](
	[TargetId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TargetTemplateId] [int] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL
CONSTRAINT [PK_TargetEmployee] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

