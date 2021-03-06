DROP DATABASE IF EXISTS [Project_GroupeB4];
CREATE DATABASE [Project_GroupeB4]

USE [Project_GroupeB4]

/****** Object:  Table [dbo].[Event]    Script Date: 17-12-20 21:59:26 ******/
CREATE TABLE [dbo].[Event]
(
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

/****** Object:  Table [dbo].[EventCategory]    Script Date: 17-12-20 21:59:26 ******/
CREATE TABLE [dbo].[EventCategory]
(
    [idEventCategory] [int] IDENTITY(1,1) NOT NULL,
    [labelCat] [varchar](50) NOT NULL,
    [color] [char](7) NOT NULL,
    CONSTRAINT [PK_EventCategory] PRIMARY KEY CLUSTERED 
(
	[idEventCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Invitation]    Script Date: 17-12-20 21:59:26 ******/
CREATE TABLE [dbo].[Invitation]
(
    [idInvitation] [int] IDENTITY(1,1) NOT NULL,
    [idPlanning] [int] NOT NULL,
    [idUserRecever] [int] NOT NULL,
    CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[idInvitation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Member]    Script Date: 17-12-20 21:59:26 ******/
CREATE TABLE [dbo].[Member]
(
    [idUser] [int] NOT NULL,
    [idPlanning] [int] NOT NULL,
    [isGranted] [bit] NOT NULL
) ON [PRIMARY]
/****** Object:  Table [dbo].[Planning]    Script Date: 17-12-20 21:59:26 ******/

CREATE TABLE [dbo].[Planning]
(
    [idPlanning] [int] IDENTITY(1,1) NOT NULL,
    [labelPlanning] [varchar](50) NOT NULL,
    [superUser] [int] NOT NULL,
    CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[idPlanning] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[User]    Script Date: 17-12-20 21:59:26 ******/
CREATE TABLE [dbo].[User]
(
    [idUser] [int] IDENTITY(1,1) NOT NULL,
    [email] [varchar](150) NOT NULL,
    [lastname] [varchar](100) NOT NULL,
    [firstname] [varchar](100) NOT NULL,
    [username] [varchar](50) NOT NULL,
    [password] [varchar](50) NOT NULL,
    CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [IX_User_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventCategory] FOREIGN KEY([idEventCategory])
REFERENCES [dbo].[EventCategory] ([idEventCategory])

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventCategory]

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])

ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])

ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Planning] FOREIGN KEY([idPlanning])
REFERENCES [dbo].[Planning] ([idPlanning])

ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])


INSERT INTO EventCategory
    (labelCat, color)
VALUES
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


INSERT INTO [User] 
    (email, lastname, firstname, username, [password])
VALUES
    ("John_Doe@gmail.com", "Doe", "John", "JDoe", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a8"),
    ("Jean_Jacques@gmail.com", "Jacques", "Jean", "JJ", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a8"),
    ("azerty@hotmail.fr", "Wenger", "Lara", "H3ll0_W0rld", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a8"),
    ("hello_kitty@gmail.com", "Kitty", "Hello", "Miaou","8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a8" );


INSERT INTO Planning
    (labelPlanning, superUser)
VALUES
    ("My Calendar", 1);

INSERT INTO Member
    (idUser, idPlanning, isGranted)
VALUES
    (2, 1, 1),
    (3, 1, 0);