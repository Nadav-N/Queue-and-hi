
USE [master]
GO
/****** Object:  Database [qnhdb]    Script Date: 17/04/2015 23:40:58 ******/
if not exists (select * from sys.databases db where name='qnhdb')
begin
	CREATE DATABASE [qnhdb] ON  PRIMARY 
	( NAME = N'qnhdb', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\qnhdb.mdf' , SIZE = 2240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
	 LOG ON 
	( NAME = N'qnhdb_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\qnhdb_log.LDF' , SIZE = 560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
end
go

USE [qnhdb]
GO


/************* drop all tables ******************/
if exists (select * from sys.tables where name='answer_rankings')
	drop table answer_rankings
go

if exists (select * from sys.tables where name='answers')
	drop table answers
go

if exists (select * from sys.tables where name='question_rankings')
	drop table question_rankings
go

if exists (select * from sys.tables where name='tags')
	drop table tags
go

if exists (select * from sys.tables where name='questions')
	drop table questions
go

if exists(select * from sys.tables where name='notifications')
	drop table notifications
go

if exists (select * from sys.tables where name='users')
	drop table users
go

USE [qnhdb]
GO

/****** Object:  Table [dbo].[notifications]    Script Date: 02/05/2015 00:36:52 ******/

CREATE TABLE [dbo].[notifications](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[notification_type] [int] NOT NULL,
	[message] [nvarchar](255) NOT NULL,
	[timestamp] [datetime] NOT NULL,
	[recipient] [int] NOT NULL,
	[seen] [tinyint] NOT NULL,
 CONSTRAINT [PK_notifications] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[answer_rankings]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[answer_rankings](
	[author_id] [int] NOT NULL,		--the ranking user, not the original author
	[answer_id] [int] NOT NULL,
	[rank] [tinyint] NOT NULL,
 CONSTRAINT [PK_answer_rankings] PRIMARY KEY CLUSTERED 
(
	[author_id] ASC,
	[answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[answers]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[answers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question_id] [int] NOT NULL,
	[contents] [nvarchar](max) NOT NULL,
	[ranking] [int] NOT NULL,
	[author_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_answers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [dbo].[question_rankings]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[question_rankings](
	[author_id] [int] NOT NULL,		--the ranking user, not the original author
	[question_id] [int] NOT NULL,
	[rank] [tinyint] NOT NULL,
 CONSTRAINT [PK_question_rankings] PRIMARY KEY CLUSTERED 
(
	[author_id] ASC,
	[question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[questions]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[questions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[contents] [nvarchar](max) NOT NULL,
	[ranking] [int] NOT NULL,
	[author_id] [int] NOT NULL,
	[recommended] [tinyint] NOT NULL,
	[created] [datetime] NOT NULL,
	[right_answer_id] [int] NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_questions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/****** Object:  Table [dbo].[tags]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[tags](
	[question_id] [int] NOT NULL,
	[tag] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tags] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC,
	[tag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[users]    Script Date: 17/04/2015 23:40:58 ******/
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[isadmin] [tinyint] NOT NULL,
	[isowner] [tinyint] NOT NULL,
	[ismuted] [tinyint] NOT NULL,
	[ranking] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[pwd] [varchar](20) NOT NULL
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Index [IX_users_email]    Script Date: 17/04/2015 23:40:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_users_email] ON [dbo].[users]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_users_name]    Script Date: 17/04/2015 23:40:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_users_name] ON [dbo].[users]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[answer_rankings]  WITH CHECK ADD  CONSTRAINT [FK_answer_rankings_answers] FOREIGN KEY([answer_id])
REFERENCES [dbo].[answers] ([id])
GO
ALTER TABLE [dbo].[answer_rankings] CHECK CONSTRAINT [FK_answer_rankings_answers]
GO
ALTER TABLE [dbo].[answer_rankings]  WITH CHECK ADD  CONSTRAINT [FK_answer_rankings_users] FOREIGN KEY([author_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[answer_rankings] CHECK CONSTRAINT [FK_answer_rankings_users]
GO
ALTER TABLE [dbo].[answers]  WITH CHECK ADD  CONSTRAINT [FK_answers_questions] FOREIGN KEY([question_id])
REFERENCES [dbo].[questions] ([id])
GO
ALTER TABLE [dbo].[answers] CHECK CONSTRAINT [FK_answers_questions]
GO
ALTER TABLE [dbo].[answers]  WITH CHECK ADD  CONSTRAINT [FK_answers_users] FOREIGN KEY([author_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[answers] CHECK CONSTRAINT [FK_answers_users]
GO
ALTER TABLE [dbo].[question_rankings]  WITH CHECK ADD  CONSTRAINT [FK_question_rankings_questions] FOREIGN KEY([question_id])
REFERENCES [dbo].[questions] ([id])
GO
ALTER TABLE [dbo].[question_rankings] CHECK CONSTRAINT [FK_question_rankings_questions]
GO
ALTER TABLE [dbo].[question_rankings]  WITH CHECK ADD  CONSTRAINT [FK_question_rankings_users] FOREIGN KEY([author_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[question_rankings] CHECK CONSTRAINT [FK_question_rankings_users]
GO
ALTER TABLE [dbo].[questions]  WITH CHECK ADD  CONSTRAINT [FK_questions_users] FOREIGN KEY([author_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[questions] CHECK CONSTRAINT [FK_questions_users]
GO
ALTER TABLE [dbo].[tags]  WITH CHECK ADD  CONSTRAINT [FK_tags_questions] FOREIGN KEY([question_id])
REFERENCES [dbo].[questions] ([id])
GO
ALTER TABLE [dbo].[tags] CHECK CONSTRAINT [FK_tags_questions]
GO
ALTER TABLE [dbo].[notifications]  WITH CHECK ADD  CONSTRAINT [FK_notifications_users] FOREIGN KEY([recipient])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[notifications] CHECK CONSTRAINT [FK_notifications_users]
GO


--sample data
--users
insert into users(name, email, isadmin, isowner, ismuted, ranking, created ,pwd)
values('moshe','moshe@microsoft.com',1,1,0,1,'20150501','123456')

insert into users(name, email, isadmin, isowner, ismuted, ranking, created,pwd)
values('david','david@openu.ac.il',1,0,0,1,'20150502','123456')

insert into users(name, email, isadmin, isowner, ismuted, ranking, created,pwd)
values('stu','stu@gmail.com',0,0,0,15,'20150511','123456')

insert into users(name, email, isadmin, isowner, ismuted, ranking, created,pwd)
values('stu2','stu2@hotmail.com',0,0,1,10,'20150510','123456')

--questions and tags

insert into questions(title, contents, ranking, author_id, recommended, created, right_answer_id, version)
values ('Entity Framework fetch Top 10 rows', 'How do I get only top ten rows from the table using linq?', 0, 2, 0, '20141204',null, 0)

declare @i int
select @i=@@identity
insert into tags values(@i,'asp.net-mvc')
insert into tags values(@i,'entity-framework')
insert into tags values(@i,'linq-to-sql linq-to-entities')
;

insert into questions(title, contents, ranking, author_id, recommended, created, right_answer_id, version)
values ('Entity Framework order by column', 'How do I do a select with Order By clause in linq?', 0, 2, 0, '20141205',null, 0)

select @i=@@identity
insert into tags values(@i,'asp.net-mvc')
insert into tags values(@i,'entity-framework')
insert into tags values(@i,'linq-to-sql linq-to-entities')
;


select * from sys.tables