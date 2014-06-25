USE [NexEPA]
GO

/****** Object:  Table [dbo].[EPA]    Script Date: 6/19/2014 10:17:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EPA](
	[EPAId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TargetId] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[FinalGrade] [varchar](1) NULL,
	[FinalScore] [decimal](18, 2) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EPA] PRIMARY KEY CLUSTERED 
(
	[EPAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

