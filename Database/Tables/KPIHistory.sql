USE [NexEPA]
GO

/****** Object:  Table [dbo].[KPIHistory]    Script Date: 5/6/2014 3:18:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[KPIHistory](
	[KPIId] [int] NOT NULL,
	[LogNo] [varchar](20) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[IsRequireComment] [bit] NULL,
	[IsDisabled] [bit] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_KPIHistory] PRIMARY KEY CLUSTERED 
(
	[KPIId] ASC,
	[LogNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

