USE [NexEPA]
GO

/****** Object:  Table [dbo].[EmployeePermisson]    Script Date: 5/6/2014 3:15:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeePermission](
	[EmployeeId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsAllow] [bit] NULL,
	[IsDeny] [bit] NULL,
 CONSTRAINT [PK_EmployeePermission] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

