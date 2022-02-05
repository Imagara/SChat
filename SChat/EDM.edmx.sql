
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/06/2022 03:45:07
-- Generated from EDMX file: C:\Users\milic\source\repos\Chat\SChat\EDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SChatDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Message_Chat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Message] DROP CONSTRAINT [FK_Message_Chat];
GO
IF OBJECT_ID(N'[dbo].[FK_Message_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Message] DROP CONSTRAINT [FK_Message_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChat_Chat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserChat] DROP CONSTRAINT [FK_UserChat_Chat];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChat_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserChat] DROP CONSTRAINT [FK_UserChat_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Chat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chat];
GO
IF OBJECT_ID(N'[dbo].[Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Message];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserChat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserChat];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Chat'
CREATE TABLE [dbo].[Chat] (
    [IdChat] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Message'
CREATE TABLE [dbo].[Message] (
    [IdMessage] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [IdChat] int  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int  NOT NULL,
    [NickName] nvarchar(30)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Birthday] datetime  NULL,
    [PhoneNumber] nvarchar(10)  NULL,
    [ProfileImgSource] nvarchar(max)  NULL
);
GO

-- Creating table 'UserChat'
CREATE TABLE [dbo].[UserChat] (
    [Id] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [IdChat] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdChat] in table 'Chat'
ALTER TABLE [dbo].[Chat]
ADD CONSTRAINT [PK_Chat]
    PRIMARY KEY CLUSTERED ([IdChat] ASC);
GO

-- Creating primary key on [IdMessage] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [PK_Message]
    PRIMARY KEY CLUSTERED ([IdMessage] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserChat'
ALTER TABLE [dbo].[UserChat]
ADD CONSTRAINT [PK_UserChat]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdChat] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [FK_Message_Chat]
    FOREIGN KEY ([IdChat])
    REFERENCES [dbo].[Chat]
        ([IdChat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_Chat'
CREATE INDEX [IX_FK_Message_Chat]
ON [dbo].[Message]
    ([IdChat]);
GO

-- Creating foreign key on [IdChat] in table 'UserChat'
ALTER TABLE [dbo].[UserChat]
ADD CONSTRAINT [FK_UserChat_Chat]
    FOREIGN KEY ([IdChat])
    REFERENCES [dbo].[Chat]
        ([IdChat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChat_Chat'
CREATE INDEX [IX_FK_UserChat_Chat]
ON [dbo].[UserChat]
    ([IdChat]);
GO

-- Creating foreign key on [IdUser] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [FK_Message_User]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_User'
CREATE INDEX [IX_FK_Message_User]
ON [dbo].[Message]
    ([IdUser]);
GO

-- Creating foreign key on [IdUser] in table 'UserChat'
ALTER TABLE [dbo].[UserChat]
ADD CONSTRAINT [FK_UserChat_User]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChat_User'
CREATE INDEX [IX_FK_UserChat_User]
ON [dbo].[UserChat]
    ([IdUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------