USE [NexEPA]
GO

/****** Object:  Table [dbo].[TargetProcessSetting]    Script Date: 3/5/2014 2:44:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetProcessSetting](
	[RefNo] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[IsEvaluatedByProjectManager] [bit] NULL,
	[IsEveluatedByEmployeeLevel] [bit] NULL,
	[LevelAmount] [tinyint] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
CONSTRAINT [PK_TargetProcessSetting] PRIMARY KEY CLUSTERED 
(
	[RefNo] ASC,
	[ProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


