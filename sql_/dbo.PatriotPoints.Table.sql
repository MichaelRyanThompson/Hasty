USE [Hasty]
GO
/****** Object:  Table [dbo].[PatriotPoints]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatriotPoints](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SourceId] [int] NOT NULL,
	[Points] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PatriotPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PatriotPoints] ADD  CONSTRAINT [DF_PatriotPoints_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[PatriotPoints]  WITH CHECK ADD  CONSTRAINT [FK_PatriotPoints_PatriotPointsSource] FOREIGN KEY([SourceId])
REFERENCES [dbo].[PatriotPointsSource] ([Id])
GO
ALTER TABLE [dbo].[PatriotPoints] CHECK CONSTRAINT [FK_PatriotPoints_PatriotPointsSource]
GO
ALTER TABLE [dbo].[PatriotPoints]  WITH CHECK ADD  CONSTRAINT [FK_Users_PatriotPoints] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PatriotPoints] CHECK CONSTRAINT [FK_Users_PatriotPoints]
GO
