USE [NexEPA]
GO

/****** Object:  Table [dbo].[EmployeeGroup]    Script Date: 3/5/2014 9:28:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmployeeGroup](
	[GroupId] [int] NULL,
	[Code] [varchar](20) NULL,
	[Name] [nchar](10) NULL,
	[LeaderId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[DepartmentId] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

