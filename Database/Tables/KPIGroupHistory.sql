USE [NexEPA]
GO

/****** Object:  Table [dbo].[KPIGroupHistory]    Script Date: 8/19/2014 4:03:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[KPIGroupHistory](
	[KPIGroupId] [int] NOT NULL,
	[LogNo] [varchar](20) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDisabled] [bit] NULL,
	[TargetTypeId] [tinyint] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_KPIGroupHistory] PRIMARY KEY CLUSTERED 
(
	[KPIGroupId] ASC,
	[LogNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

