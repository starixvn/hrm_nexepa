USE [NexEPA]
GO

/****** Object:  Table [dbo].[ProcessType]    Script Date: 3/5/2014 2:14:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProcessType](
	[ProcessTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Priority] [tinyint] NULL,
	[IsDisabled] [bit] NULL
CONSTRAINT [PK_ProcessTypeId] PRIMARY KEY CLUSTERED 
(
	[ProcessTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO