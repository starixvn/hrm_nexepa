USE [NexEPA]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 3/5/2014 9:12:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[DomainUsername] [varchar](50) NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[NickName] [nvarchar](20) NOT NULL,
	[Email] [varchar](50) NULL,
	[Skype] [varchar](100) NULL,
	[GroupId] [int] NOT NULL,
	[EmployeeLevelId] [int] NOT NULL,
	[JobTitleId] [int] NULL,
	[JoinedDate] [smalldatetime] NULL,
	[IsDisabled] [bit] NULL
CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO