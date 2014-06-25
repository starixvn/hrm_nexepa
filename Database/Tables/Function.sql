USE [NexEPA]
GO

/****** Object:  Table [dbo].[Function]    Script Date: 6/19/2014 10:18:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Function](
	[FunctionId] [int] IDENTITY(1,1) NOT NULL,
	[FunctionKey] [varchar](50) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[ParentFunctionId] [int] NULL,
	[IsDisabled] [bit] NULL,
	[IsReadOnly] [bit] NULL,
 CONSTRAINT [PK_FunctionId] PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

