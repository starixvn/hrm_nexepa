USE [NexEPA]
GO

/****** Object:  Table [dbo].[ProjectRole]    Script Date: 3/5/2014 2:25:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectRole](
	[ProjectRoleId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsDisabled] [bit] NULL
CONSTRAINT [PK_ProjectRoleId] PRIMARY KEY CLUSTERED 
(
	[ProjectRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

