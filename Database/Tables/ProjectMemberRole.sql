USE [NexEPA]
GO

/****** Object:  Table [dbo].[ProjectMemberRole]    Script Date: 5/6/2014 3:20:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectMemberRole](
	[ProjectId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ProjectRoleId] [int] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectMemberRole] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[EmployeeId] ASC,
	[ProjectRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

