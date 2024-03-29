USE [Hasty]
GO
/****** Object:  Table [dbo].[PatriotPointsSource]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatriotPointsSource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PointsAwarded] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[IsExpired] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_PatriotPointsSource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PatriotPointsSource] ADD  CONSTRAINT [DF_PatriotPointsSource_Is Expired]  DEFAULT ((0)) FOR [IsExpired]
GO
ALTER TABLE [dbo].[PatriotPointsSource] ADD  CONSTRAINT [DF_PatriotPointsSource_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PatriotPointsSource] ADD  CONSTRAINT [DF_PatriotPointsSource_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[PatriotPointsSource] ADD  CONSTRAINT [DF_PatriotPointsSource_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
