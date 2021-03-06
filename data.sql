USE [ReciveMailTestDb]
GO
/****** Object:  Table [dbo].[ReceivedMail]    Script Date: 8.03.2018 14:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReceivedMail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageId] [nvarchar](200) NOT NULL,
	[Uid] [varchar](200) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[SendBy] [nvarchar](200) NOT NULL,
	[Cc] [nvarchar](max) NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_ReceivedMail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
