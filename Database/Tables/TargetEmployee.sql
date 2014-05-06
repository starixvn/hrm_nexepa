USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetEmployee]    Script Date: 5/6/2014 3:22:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetEmployee](
	[TargetId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TargetTemplateId] [int] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
 CONSTRAINT [PK_TargetEmployee] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

