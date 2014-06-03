USE [NexEPA]
GO

/****** Object:  Table [dbo].[Menu]    Script Date: 6/3/2014 3:46:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[Menu]
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Url] [varchar](100) NULL,
	[ParentId] [int] NULL,
	[FunctionId] [int] NOT NULL,
	[DisplayOrder] [int] NULL,
	[IsDisabled] [bit] NULL,
CONSTRAINT [PK_MenuId] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

