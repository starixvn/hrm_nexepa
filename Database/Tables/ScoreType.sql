USE [NexEPA]
GO

/****** Object:  Table [dbo].[ScoreType]    Script Date: 5/6/2014 3:21:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScoreType](
	[ScoreTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsDefault] [bit] NULL,
	[IsDisabled] [bit] NULL,
 CONSTRAINT [PK_ScoreTypeId] PRIMARY KEY CLUSTERED 
(
	[ScoreTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

