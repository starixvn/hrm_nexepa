USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetEmployeeSetting]    Script Date: 8/19/2014 3:43:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetEmployeeSetting](
	[EmployeeId] [int] NOT NULL,
	[EPATypeId] [int] NOT NULL,
	[TargetTemplateId] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TargetEmployeeSetting] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[EPATypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

