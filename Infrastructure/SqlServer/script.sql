USE [master]
GO
/****** Object:  Database [Project_GroupeB4]    Script Date: 02-11-20 15:47:30 ******/
CREATE DATABASE [Project_GroupeB4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project_GroupeB4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Project_GroupeB4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Project_GroupeB4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Project_GroupeB4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Project_GroupeB4] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project_GroupeB4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project_GroupeB4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project_GroupeB4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project_GroupeB4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Project_GroupeB4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project_GroupeB4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Project_GroupeB4] SET  MULTI_USER 
GO
ALTER DATABASE [Project_GroupeB4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project_GroupeB4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project_GroupeB4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project_GroupeB4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Project_GroupeB4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Project_GroupeB4] SET QUERY_STORE = OFF
GO
USE [Project_GroupeB4]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[idChat] [int] IDENTITY(1,1) NOT NULL,
	[idPlanning] [int] NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[idChat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[idEvent] [int] IDENTITY(1,1) NOT NULL,
	[idPlanning] [int] NOT NULL,
	[idEventCategory] [int] NOT NULL,
	[labelEvent] [varchar](50) NOT NULL,
	[startEvent] [datetime] NOT NULL,
	[endEvent] [datetime] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[idEvent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventCategory](
	[idEventCategory] [int] IDENTITY(1,1) NOT NULL,
	[labelCat] [varchar](50) NOT NULL,
	[color] [char](7) NOT NULL,
 CONSTRAINT [PK_EventCategory] PRIMARY KEY CLUSTERED 
(
	[idEventCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitation](
	[idInvitation] [int] IDENTITY(1,1) NOT NULL,
	[idPlanning] [int] NOT NULL,
	[idUserRecever] [int] NOT NULL,
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[idInvitation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[idPlanning] [int] NOT NULL,
	[isGranted] [bit] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[idMessage] [int] IDENTITY(1,1) NOT NULL,
	[idChat] [int] NOT NULL,
	[idUser] [int] NOT NULL,
	[body] [varchar](280) NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[idMessage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planning]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planning](
	[idPlanning] [int] IDENTITY(1,1) NOT NULL,
	[labelPlanning] [varchar](50) NOT NULL,
	[superUser] [int] NOT NULL,
 CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[idPlanning] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 02-11-20 15:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](150) NOT NULL,
	[lastname] [varchar](100) NOT NULL,
	[firstname] [varchar](100) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC,
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Planning]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventCategory] FOREIGN KEY([idEventCategory])
REFERENCES [dbo].[EventCategory] ([idEventCategory])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventCategory]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Planning]
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_Planning]
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_User] FOREIGN KEY([idUserRecever])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_User]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Planning]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Chat] FOREIGN KEY([idChat])
REFERENCES [dbo].[Chat] ([idChat])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Chat]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User]
GO
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Planning_User] FOREIGN KEY([superUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Planning_User]
GO
USE [master]
GO
ALTER DATABASE [Project_GroupeB4] SET  READ_WRITE 
GO


INSERT INTO EventCategory(labelCat, color) VALUES
 ('Anniversaire', '#1cfce6'),
 ('Rendez-vous', '#1d1d6b'),
 ('Réunion', '#c41820'),
 ('Jour férié', '#828282'),
 ('Travail', '#108008'),
 ('Etude', '#e7ff33'),
 ('Sport', '#ff12e8'),
 ('Repas', '#c47525'),
 ('Visite', '#144503'),
 ('Déplacement', '#729c78');

