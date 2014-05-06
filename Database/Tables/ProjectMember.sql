USE [NexEPA]
GO

/****** Object:  Table [dbo].[ProjectMember]    Script Date: 5/6/2014 3:20:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectMember](
	[ProjectId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[JoinDate] [datetime] NULL,
	[IsDisabled] [bit] NULL,
	[DisabledDate] [datetime] NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectMember] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

