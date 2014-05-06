USE [NexEPA]
GO

/****** Object:  Table [dbo].[EmployeeLevel]    Script Date: 5/6/2014 3:15:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeLevel](
	[EmployeeLevelId] [int] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[IsDisabled] [bit] NULL,
 CONSTRAINT [PK_EmployeeLevel] PRIMARY KEY CLUSTERED 
(
	[EmployeeLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

