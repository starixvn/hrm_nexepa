USE [NexEPA]
GO

/****** Object:  Table [dbo].[Target]    Script Date: 6/19/2014 10:16:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
DROP TABLE [dbo].[Target]
CREATE TABLE [dbo].[Target](
	[TargetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TargetTypeId] [tinyint] NOT NULL,
	[ScoreTypeId] [tinyint] NOT NULL,
	[ProcessGroupId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [tinyint] NULL,
	[KPICount] [int] NULL,
	[MemberCount] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreateFrom] [tinyint] NOT NULL,
 CONSTRAINT [PK_TargetId] PRIMARY KEY CLUSTERED 
(
	[TargetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Target] ADD  CONSTRAINT [DF_Target_CreateFrom]  DEFAULT ((1)) FOR [CreateFrom]
GO

